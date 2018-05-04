using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using SmartEngine.Core;
namespace SmartEngine.Network.Map
{
    /// <summary>
    /// 地图类
    /// </summary>
    /// <typeparam name="T">地图广播事件类型的枚举，其0,1必须为APPEAR, DISAPPEAR</typeparam>    
    public abstract class Map<T>
    {
        protected string name;
        protected uint id;

        protected ushort width, height;
        public uint ID { get { return this.id; } set { this.id = value; } }
        public uint InstanceID { get; set; }
        public string Name { get { return this.name; } }

        public ushort Width { get { return this.width; } }
        public ushort Height { get { return this.height; } }

        protected ConcurrentDictionary<ulong, Actor> actorsByID;
        protected ConcurrentDictionary<int, ConcurrentDictionary<ulong, Actor>> actorsByRegion;
        //private Dictionary<string, ActorPC> pcByName;
        const ulong ACTORID_NPC_BORDER = 0x4000000000000;
        const ulong ACTORID_ITEM_BORDER = 0x90000000000000;
        const ulong ACTORID_CORPSE_BORDER = 0xB0000000000000;
        const ulong ACTORID_PC_BORDER = 0x1000000000000;
        public static uint MAX_SIGHT_RANGE = 1000;

        private ulong nextPCID, nextNPCID, nextItemID, nextCorpseID;

        public enum EVENT_TYPE
        {
            APPEAR, DISAPPEAR
        }

        public enum TOALL_EVENT_TYPE { CHAT };

        public Map(uint id)
        {
            this.id = id;
            this.nextPCID = ACTORID_PC_BORDER + 1;
            this.nextNPCID = ACTORID_NPC_BORDER + 1;
            this.nextItemID = ACTORID_ITEM_BORDER + 1;
            this.nextCorpseID = ACTORID_CORPSE_BORDER + 1;
            this.actorsByID = new ConcurrentDictionary<ulong, Actor>();
            this.actorsByRegion = new ConcurrentDictionary<int, ConcurrentDictionary<ulong, Actor>>();
        }

        /// <summary>
        /// 取得指定Actor周围一个随机坐标
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public float[] GetRandomPosAroundActor(Actor actor)
        {
            float[] ret = new float[2];

            ret[0] = (float)Global.Random.Next((int)actor.X - 50, (int)actor.X + 50);
            ret[1] = (float)Global.Random.Next((int)actor.Y - 50, (int)actor.Y + 50);

            return ret;
        }

        /// <summary>
        /// 取得当前地图的Actor列表
        /// </summary>        
        public ConcurrentDictionary<ulong, Actor> Actors { get { return this.actorsByID; } }

        /// <summary>
        /// 按照ID取得Actor
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Actor</returns>
        public Actor GetActor(ulong id)
        {
            try
            {
                Actor result;
                actorsByID.TryGetValue(id, out result);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual ulong GetNewActorID(ActorType type)
        {
            ulong newID = 0;
            ulong startID = 0;
            switch (type)
            {
                case ActorType.PC:
                    newID = nextPCID;
                    startID = nextPCID;
                    if (newID >= UInt64.MaxValue)
                        newID = ACTORID_PC_BORDER + 1;
                    break;
                case ActorType.NPC:
                    newID = nextNPCID;
                    startID = nextNPCID;
                    if (newID >= UInt64.MaxValue)
                        newID = ACTORID_NPC_BORDER + 1;
                    break;
                case ActorType.CORPSE:
                    newID = nextCorpseID;
                    startID = nextCorpseID;
                    if (newID >= UInt64.MaxValue)
                        newID = ACTORID_CORPSE_BORDER + 1;
                    break;
                default:
                    newID = nextItemID;
                    startID = nextItemID;
                    if (newID >= UInt64.MaxValue)
                        newID = ACTORID_ITEM_BORDER + 1;
                    break;
            }
            

            while (this.actorsByID.ContainsKey(newID))
            {
                newID++;

                if (newID >= UInt64.MaxValue)
                {
                    switch (type)
                    {
                        case ActorType.PC:
                            newID = ACTORID_PC_BORDER + 1;
                            break;
                        case ActorType.NPC:
                            newID = ACTORID_NPC_BORDER + 1;
                            break;
                        case ActorType.CORPSE:
                            newID = ACTORID_CORPSE_BORDER + 1;
                            break;
                        default:
                            newID = ACTORID_ITEM_BORDER + 1;
                            break;
                    }
                }
                if (newID == startID) return 0;
            }
            switch (type)
            {
                case ActorType.PC:
                    nextPCID = newID + 1;
                    break;
                case ActorType.NPC:                    
                    nextNPCID = newID + 1;
                    break;
                case ActorType.CORPSE:
                    nextCorpseID = newID + 1;
                    break;
                default:
                    nextItemID = newID + 1;
                    break;
            }
            newID |= (ulong)InstanceID << 32;
            return newID;
        }

        /// <summary>
        /// 注册一个Actor，并自动赋予一个ActorID
        /// </summary>
        /// <param name="nActor">需注册的Actor</param>
        /// <returns>是否成功</returns>        
        public bool RegisterActor(Actor nActor)
        {
            // default: no success
            bool succes = false;
            if (actorsByID.ContainsKey(nActor.ActorID))
                return succes;
            // set the actorID and the actor's region on this map
            ulong newID = 0;
            lock (this)
                newID = this.GetNewActorID(nActor.ActorType);

            if (newID != 0)
            {
                nActor.ActorID = newID;
                nActor.Region = this.GetRegion(nActor.X, nActor.Y);

                // make the actor invisible (when the actor is ready: set it to false & call OnActorVisibilityChange)
                nActor.Invisible = true;

                // add the new actor to the tables
                this.actorsByID[nActor.ActorID] = nActor;

                ConcurrentDictionary<ulong, Actor> region;
                if (!this.actorsByRegion.TryGetValue(nActor.Region, out region))
                {
                    region = new ConcurrentDictionary<ulong, Actor>();
                    this.actorsByRegion[nActor.Region] = region;
                }

                region[nActor.ActorID] = nActor;

                succes = OnRegister(nActor);
            }
            nActor.MapID = this.ID;
            nActor.MapInstanceID = this.InstanceID;
            nActor.EventHandler.OnCreate(succes);
            return succes;
        }

        /// <summary>
        /// 按照给定的ActorID注册一个Actor
        /// </summary>
        /// <param name="nActor">需注册的Actor</param>
        /// <param name="actorID">指定的ActorID，若已经有重复则返回false</param>
        /// <returns>是否成功</returns>
        
        public bool RegisterActor(Actor nActor, ulong actorID)
        {
            // default: no success
            bool succes = false;

            // set the actorID and the actor's region on this map
            ulong newID = actorID;

            if (newID != 0)
            {
                nActor.ActorID = newID;
                nActor.Region = this.GetRegion(nActor.X, nActor.Y);
                // make the actor invisible (when the actor is ready: set it to false & call OnActorVisibilityChange)
                nActor.Invisible = true;

                // add the new actor to the tables
                if (!this.actorsByID.ContainsKey(nActor.ActorID)) this.actorsByID[nActor.ActorID] = nActor;
                else
                    return false;

                ConcurrentDictionary<ulong, Actor> region;
                if (!this.actorsByRegion.TryGetValue(nActor.Region, out region))
                {
                    region = new ConcurrentDictionary<ulong, Actor>();
                    this.actorsByRegion[nActor.Region] = region;
                }
                region[nActor.ActorID] = nActor;
                succes = OnRegister(nActor);
            }

            nActor.MapID = this.ID;
            nActor.MapInstanceID = this.InstanceID;
            nActor.EventHandler.OnCreate(succes);
            return succes;
        }

        public abstract bool OnRegister(Actor actor);

        /// <summary>
        /// 广播Actor可见度已改变
        /// </summary>
        /// <param name="dActor">可见度改变的Actor</param>
        public void OnActorVisibilityChange(Actor dActor)
        {
            if (dActor.Invisible)
            {
                dActor.Invisible = false;
                this.SendEventToAllActorsWhoCanSeeActor((T)(object)EVENT_TYPE.DISAPPEAR, null, dActor, false);
                dActor.Invisible = true;
            }

            else
                this.SendEventToAllActorsWhoCanSeeActor((T)(object)EVENT_TYPE.APPEAR, null, dActor, true);
        }

        /// <summary>
        /// 从地图中删除某指定Actor
        /// </summary>
        /// <param name="dActor"></param>
        /// <param name="deleteOnly">是否仅仅删除但是并不广播</param>
        public void DeleteActor(Actor dActor,bool deleteOnly=false)
        {
            if (!deleteOnly)
                this.SendEventToAllActorsWhoCanSeeActor((T)(object)EVENT_TYPE.DISAPPEAR, null, dActor, false);
            Actor obj;
            this.actorsByID.TryRemove(dActor.ActorID, out obj);

            ConcurrentDictionary<ulong, Actor> region;
            if (this.actorsByRegion.TryGetValue(dActor.Region,out region))
            {
                Actor removed;
                region.TryRemove(dActor.ActorID, out removed);
            }

            OnDeleteActor(dActor);
            if (dActor.EventHandler != null & !deleteOnly)
                dActor.EventHandler.OnDelete();
        }

        /// <summary>
        /// 删除Actor时触发的事件
        /// </summary>
        /// <param name="actor"></param>
        public abstract void OnDeleteActor(Actor actor);

        /// <summary>
        /// 移动Actor
        /// </summary>
        /// <param name="mActor"></param>
        /// <param name="arg"></param>
        public void MoveActor(Actor mActor, MoveArg arg)
        {
            MoveActor(mActor, arg, false);
        }

        /// <summary>
        /// 移动Actor
        /// </summary>
        /// <param name="mActor"></param>
        /// <param name="arg"></param>
        /// <param name="sendToSelf"></param>
        public void MoveActor(Actor mActor, MoveArg arg, bool sendToSelf)
        {

            try
            {
                bool knockBack = false;

                // check wheter the destination is in range, if not kick the client
                /*if (!this.MoveStepIsInRange(mActor, arg))
                {
                    knockBack = true;
                    sendToSelf = true;
                }*/
                if (mActor.EventHandler == null)
                    return;
                OnMoveActor(mActor, arg, knockBack);

                //scroll through all actors that "could" see the mActor at "from"
                //or are going "to see" mActor, or are still seeing mActor
                if (!knockBack)
                {
                    for (short deltaY = -1; deltaY <= 1; deltaY++)
                    {
                        for (short deltaX = -1; deltaX <= 1; deltaX++)
                        {
                            int region = (int)(mActor.Region + (deltaX * 10000) + deltaY);
                            ConcurrentDictionary<ulong, Actor> actors;
                            if (!this.actorsByRegion.TryGetValue(region, out actors)) continue;

                            foreach (KeyValuePair<ulong, Actor> pair in actors)
                            {
                                Actor actor = pair.Value;
                                try
                                {
                                    if (actor == null) continue;
                                    if (actor.EventHandler == null)
                                    {
                                        DeleteActor(actor);
                                        continue;
                                    }
                                    if (actor.ActorID == mActor.ActorID && !sendToSelf) continue;

                                    // A) INFORM OTHER ACTORS

                                    //actor "could" see mActor at its "from" position
                                    if (this.ACanSeeB(actor, mActor))
                                    {
                                        //actor will still be able to see mActor
                                        if (this.ACanSeeB(actor, mActor, arg.X, arg.Y))
                                        {
                                            if (arg.MoveType == MoveType.Start)
                                                actor.EventHandler.OnActorStartsMoving(mActor, arg);
                                            else
                                                actor.EventHandler.OnActorStopsMoving(mActor, arg);
                                        }
                                        //actor won't be able to see mActor anymore
                                        else actor.EventHandler.OnActorDisappears(mActor);
                                    }
                                    //actor "could not" see mActor, but will be able to see him now
                                    else if (this.ACanSeeB(actor, mActor, arg.X, arg.Y))
                                    {
                                        actor.EventHandler.OnActorAppears(mActor);

                                        //send move / move stop
                                        if (arg.MoveType == MoveType.Start)
                                            actor.EventHandler.OnActorStartsMoving(mActor, arg);
                                        else
                                            actor.EventHandler.OnActorStopsMoving(mActor, arg);
                                    }

                                    // B) INFORM mActor
                                    //mActor "could" see actor on its "from" position
                                    if (this.ACanSeeB(mActor, actor))
                                    {
                                        //mActor won't be able to see actor anymore
                                        if (!this.ACanSeeB(mActor, arg.X, arg.Y, actor))
                                            mActor.EventHandler.OnActorDisappears(actor);
                                        //mAactor will still be able to see actor
                                        else { }
                                    }

                                    else if (this.ACanSeeB(mActor, arg.X, arg.Y, actor))
                                    {
                                        //mActor "could not" see actor, but will be able to see him now
                                        //send pcinfo
                                        mActor.EventHandler.OnActorAppears(actor);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.ShowError(ex);
                                }
                            }
                        }
                    }
                }
                else
                    mActor.EventHandler.OnActorStopsMoving(mActor, arg);
                //lock (mActor)
                {
                    mActor.X = arg.X;
                    mActor.Y = arg.Y;
                    mActor.Z = arg.Z;

                    mActor.Dir = arg.Dir;
                    //mActor.Speed = arg.Speed;
                }
                //update the region of the actor
                int newRegion = this.GetRegion(arg.X, arg.Y);
                if (mActor.Region != newRegion)
                {
                    ConcurrentDictionary<ulong, Actor> list;
                    if (this.actorsByRegion.TryGetValue(mActor.Region, out list))
                    {
                        Actor removed;
                        list.TryRemove(mActor.ActorID, out removed);
                    }
                    //turn off all the ai if the old region has no player on it
                    mActor.Region = newRegion;

                    if (!this.actorsByRegion.TryGetValue(newRegion, out list))
                    {
                        list = new ConcurrentDictionary<ulong, Actor>();
                        this.actorsByRegion[newRegion] = list;
                    }

                    list[mActor.ActorID] = mActor;
                }

            }

            catch (Exception ex)
            { Logger.ShowError(ex); }

        }

        public abstract void OnMoveActor(Actor mActor, MoveArg arg, bool knockBack);

        /// <summary>
        /// 取得某个地区玩家Actor总数
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public int GetRegionPlayerCount(int region)
        {
            ConcurrentDictionary<ulong, Actor> actors;
            int count = 0;
            if (this.actorsByRegion.TryGetValue(region, out actors))
            {
                foreach (KeyValuePair<ulong,Actor> actor in actors)
                {
                    if (actor.Value.ActorType == ActorType.PC) count++;
                }
                return count;
            }
            else return 0;
        }

        /// <summary>
        /// 检查移动是否合法
        /// </summary>
        /// <param name="mActor"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public virtual bool MoveStepIsInRange(Actor mActor, MoveArg to)
        {
            if (mActor.ActorType == ActorType.PC)
            {
                TimeSpan span = DateTime.Now - mActor.moveCheckStamp;
                if (span.TotalMilliseconds > 50)
                {

                    double maximal;
                    if (span.TotalMilliseconds > 1000)
                        maximal = mActor.Speed * 2f;
                    else
                        maximal = mActor.Speed * (span.TotalMilliseconds / 1000) * 2f;
                    // Disabled, until we have something better
                    if (System.Math.Abs(mActor.X - to.X) > maximal)
                        return false;
                    if (System.Math.Abs(mActor.Y - to.Y) > maximal)
                        return false;
                    //we don't check for z , yet, to allow falling from great hight
                    //if (System.Math.Abs(mActor.z - to[2]) > mActor.maxMoveRange) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 根据坐标生成区域ID
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetRegion(int x, int y)
        {

            uint REGION_DIAMETER = MAX_SIGHT_RANGE * 2;

            // best case we should now load the size of the map from a config file, however that's not
            // possible yet, so we just create a region code off the values x/y

            /*
            values off x/y are like:
            x = -20 500.0f
            y =   1 000.0f
            
            before we convert them to uints we make them positive, and store the info wheter they were negative
            x  = - 25 000.0f;
            nx = 1;
            y  =  1 000.0f;
            ny = 0;
            
            no we convert them to uints
             
            ux = 25 000;
            nx = 1;
            uy =  1 000;
            ny = 0;
            
            now we do ux = (uint) ( ux / REGION_DIAMETER ) [the same for uy]
            we have:
             
            ux = 2;
            nx = 1;
            uy = 0;
            ny = 0;
             
            off this data we generate the region code:
             > we use a uint as region code
             > max value of an uint32 is 4 294 967 295
             > the syntax of the region code is ux[5digits].uy[5digits]
             if(!nx) ux = ux + 50000;
             else ux = 50000 - ux;
             if(!ny) uy = uy + 50000;
             else uy = 50000 - uy;
  
            uint regionCode = 49998 50001
            uint regionCode = 4999850001

            Note: 
             We inform an Actor(Player) about all other Actors in its own region and the 8 regions around
             this region. Because of this REGION_DIAMETER has to be MAX_SIGHT_RANGE (or greater).
             Also check SVN/SagaMap/doc/mapRegions.bmp
            */
            // init nx,ny
            bool nx = false;
            bool ny = false;
            // make x,y positive
            if (x < 0) { x = x - (2 * x); nx = true; }
            if (y < 0) { y = y - (2 * y); ny = true; }
            // convert x,y to uints
            int ux = (int)x;
            int uy = (int)y;
            // divide through REGION_DIAMETER
            ux = (int)(ux / REGION_DIAMETER);
            uy = (int)(uy / REGION_DIAMETER);
            // calc ux
            if (ux > 4999) ux = 4999;
            if (!nx) ux = ux + 5000;
            else ux = 5000 - ux;
            // calc uy
            if (uy > 4999) uy = 4999;
            if (!ny) uy = uy + 5000;
            else uy = 5000 - uy;
            // finally generate the region code and return it
            return (int)((ux * 10000) + uy);
        }

        public bool ACanSeeB(Actor A, Actor B)
        {
            if (A == null || B == null)
                return false;
            if (B.Invisible) return false;
            int difX = A.X - B.X;
            int difY = A.Y - B.Y;
            if (difX > int.MinValue && difY > int.MinValue)
            {
                if (System.Math.Abs(difX) > A.SightRange) return false;
                if (System.Math.Abs(difY) > A.SightRange) return false;
                return true;
            }
            else return false; 
        }

        public bool ACanSeeB(Actor A, Actor B, int bx, int by)
        {
            if (A == null || B == null)
                return false;
            if (B.Invisible) return false;
            int difX = A.X - bx;
            int difY = A.Y - by;
            if (difX > int.MinValue && difY > int.MinValue)
            {
                if (System.Math.Abs(difX) > A.SightRange) return false;
                if (System.Math.Abs(difY) > A.SightRange) return false;
                return true;
            }
            else return false;
        }

        public bool ACanSeeB(Actor A, int ax, int ay, Actor B)
        {
            if (A == null || B == null)
                return false;
            if (B.Invisible) return false;
            int difX = ax - B.X;
            int difY = ay - B.Y;
            if (difX > int.MinValue && difY > int.MinValue)
            {
                if (System.Math.Abs(difX) > A.SightRange) return false;
                if (System.Math.Abs(difY) > A.SightRange) return false;
                return true;
            }
            else return false;
        }

        public bool ACanSeeB(Actor A, Actor B, int sightrange)
        {
            if (A == null || B == null)
                return false;
            if (B.Invisible) return false;
            int difX = A.X - B.X;
            int difY = A.Y - B.Y;
            if (difX > int.MinValue && difY > int.MinValue)
            {
                if (System.Math.Abs(difX) > sightrange) return false;
                if (System.Math.Abs(difY) > sightrange) return false;
                return true;
            }
            else return false; 
        }

        /// <summary>
        /// 广播某Actor的可见度
        /// </summary>
        /// <param name="jActor"></param>
        public void SendVisibleActorsToActor(Actor jActor)
        {
            List<Actor> visible = new List<Actor>();
            //search all actors which can be seen by jActor and tell jActor about them
            for (short deltaY = -1; deltaY <= 1; deltaY++)
            {
                for (short deltaX = -1; deltaX <= 1; deltaX++)
                {
                    int region = (int)(jActor.Region + (deltaX * 10000) + deltaY);
                    ConcurrentDictionary<ulong, Actor> actors;
                    if (!this.actorsByRegion.TryGetValue(region, out actors)) continue;
                    foreach (KeyValuePair<ulong, Actor> pair in actors)
                    {
                        Actor actor = pair.Value;
                        try
                        {
                            //if (actor.ActorID == jActor.ActorID) continue;
                            //check wheter jActor can see actor, if yes: inform jActor
                            if (this.ACanSeeB(jActor, actor))
                            {
                                visible.Add(actor);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                    }
                }
            }

            jActor.EventHandler.OnGotVisibleActors(visible);
        }

        /// <summary>
        /// 瞬移某Actor
        /// </summary>
        /// <param name="sActor"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void TeleportActor(Actor sActor, int x, int y, int z)
        {
            OnTeleportActor(sActor, x, y, z);
            this.SendEventToAllActorsWhoCanSeeActor((T)(object)EVENT_TYPE.DISAPPEAR, null, sActor, false);

            Actor removed;
            this.actorsByRegion[sActor.Region].TryRemove(sActor.ActorID, out removed);

            //lock (sActor)
            {
                sActor.X = x;
                sActor.Y = y;
                sActor.Region = this.GetRegion(x, y);
            }

            ConcurrentDictionary<ulong, Actor> region;
            if (!this.actorsByRegion.TryGetValue(sActor.Region, out region))
            {
                region = new ConcurrentDictionary<ulong, Actor>();
                this.actorsByRegion[sActor.Region] = region;
            }
            region[sActor.ActorID] = sActor;

            sActor.EventHandler.OnTeleport(x, y, z);

            this.SendEventToAllActorsWhoCanSeeActor((T)(object)EVENT_TYPE.APPEAR, null, sActor, false);
            this.SendVisibleActorsToActor(sActor);
        }

        public abstract void OnTeleportActor(Actor sActor, float x, float y, float z);

        /// <summary>
        /// 广播事件给可见Actor
        /// </summary>
        /// <param name="etype">事件类型</param>
        /// <param name="args">参数</param>
        /// <param name="sActor">触发事件的Actor</param>
        /// <param name="sendToSourceActor">是否广播给源Actor</param>
        public void SendEventToAllActorsWhoCanSeeActor(T etype, MapEventArgs args, Actor sActor, bool sendToSourceActor)
        {
            try
            {
                for (short deltaY = -1; deltaY <= 1; deltaY++)
                {
                    for (short deltaX = -1; deltaX <= 1; deltaX++)
                    {
                        int region = (int)(sActor.Region + (deltaX * 10000) + deltaY);
                        ConcurrentDictionary<ulong, Actor> actors;
                        if (!this.actorsByRegion.TryGetValue(region, out actors)) continue;
                        foreach (KeyValuePair<ulong, Actor> pair in actors)
                        {
                            try
                            {
                                Actor actor = pair.Value;
                                if (actor == null)
                                    continue;
                                if (!sendToSourceActor && (actor.ActorID == sActor.ActorID)) continue;
                                if (actor.EventHandler == null)
                                {
                                    DeleteActor(actor, true);
                                    continue;
                                }
                                if (this.ACanSeeB(actor, sActor))
                                {
                                    OnEvent(etype, args, actor, sActor);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// 取得指定坐标周围指定距离内的所有Actor
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="range">范围</param>
        /// <returns>范围内的Actor列表</returns>
        public List<Actor> GetActorsAroundArea(int x, int y,int z, int range)
        {
            List<Actor> res = new List<Actor>();
            try
            {

                for (short deltaY = -1; deltaY <= 1; deltaY++)
                {
                    for (short deltaX = -1; deltaX <= 1; deltaX++)
                    {
                        int region = (int)(GetRegion(x, y) + (deltaX * 10000) + deltaY);
                        ConcurrentDictionary<ulong, Actor> actors;
                        if (!this.actorsByRegion.TryGetValue(region, out actors)) continue;
                        foreach (KeyValuePair<ulong, Actor> pair in actors)
                        {
                            Actor actor = pair.Value;
                            try
                            {
                                if (actor.EventHandler == null)
                                {
                                    DeleteActor(actor, true);
                                    continue;
                                }
                                if (actor.DistanceToPoint(x, y, z) <= range)
                                {
                                    res.Add(actor);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            return res;
        }

        /// <summary>
        /// 取得指定Actor周围指定距离内的所有Actor
        /// </summary>
        /// <param name="sActor">源Actor</param>
        /// <param name="range">范围</param>
        /// <param name="includeSourceActor">是否包含源Actor</param>
        /// <returns>范围内的Actor列表</returns>
        public List<Actor> GetActorsAroundActor(Actor sActor, int range, bool includeSourceActor)
        {
            List<Actor> res = new List<Actor>();
            try
            {
                for (short deltaY = -1; deltaY <= 1; deltaY++)
                {
                    for (short deltaX = -1; deltaX <= 1; deltaX++)
                    {
                        int region = (int)(sActor.Region + (deltaX * 10000) + deltaY);
                        ConcurrentDictionary<ulong, Actor> actors;
                        if (!this.actorsByRegion.TryGetValue(region, out actors)) continue;
                        foreach (KeyValuePair<ulong, Actor> pair in actors)
                        {
                            Actor actor = pair.Value;
                            try
                            {
                                if (!includeSourceActor && (actor.ActorID == sActor.ActorID)) continue;
                                if (sActor.DistanceToActor(actor) <= range)
                                {
                                    res.Add(actor);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            return res;
        }

        /// <summary>
        /// 当广播事件时调用的方法
        /// </summary>
        /// <param name="etype">事件类型</param>
        /// <param name="args">参数</param>
        /// <param name="sActor">触发事件的源Actor</param>
        /// <param name="dActor">触发事件的目标Actor</param>
        public abstract void OnEvent(T etype, MapEventArgs args, Actor sActor, Actor dActor);

        /// <summary>
        /// 广播事件给所有Actor
        /// </summary>
        /// <param name="etype">事件类型</param>
        /// <param name="args">参数</param>
        /// <param name="sActor">触发事件的源Actor</param>
        /// <param name="sendToSourceActor">是否广播给源Actor</param>
        public void SendEventToAllActors(T etype, MapEventArgs args, Actor sActor, bool sendToSourceActor)
        {
            foreach (Actor actor in this.actorsByID.Values)
            {
                if (actor.EventHandler == null)
                {
                    DeleteActor(actor);
                    continue;
                }
                if (sActor != null) if (!sendToSourceActor && (actor.ActorID == sActor.ActorID)) continue;

                OnEvent(etype, args, actor, sActor);
            }
        }

        /// <summary>
        /// 将指定Actor传送到另外一个地图的某个坐标
        /// </summary>
        /// <param name="mActor"></param>
        /// <param name="newMap">新地图</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SendActorToMap(Actor mActor, Map<T> newMap, int x, int y, int z)
        {
            // obtain the new map
            if (newMap == null)
                return;
            // delete the actor from this map
            this.DeleteActor(mActor);

            // update the actor
            //lock (mActor)
            {
                mActor.MapID = newMap.ID;
                mActor.X = x;
                mActor.Y = y;
                mActor.Z = z;
            }

            // register the actor in the new map
            if (mActor.ActorType != ActorType.PC)
            {
                newMap.RegisterActor(mActor);
            }
            else
            {
                newMap.RegisterActor(mActor, mActor.ActorID);
            }
        }

        private void SendActorToActor(Actor mActor, Actor tActor)
        {
            int x, y, z;
            //lock (tActor)
            {
                x = tActor.X;
                y = tActor.Y;
                z = tActor.Z;
            }
            if (mActor.MapID == tActor.MapID)
            {
                this.TeleportActor(mActor, x, y, z);
            }
            else
                this.SendActorToMap(mActor, GetMapOfActor(tActor), x, y, z);
        }

        protected abstract Map<T> GetMapOfActor(Actor actor);
        
    }
}
