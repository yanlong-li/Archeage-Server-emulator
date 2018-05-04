using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Skills
{
    public enum SkillMode
    {
        Cast = 0,
        CastActionDelay = 1,
        Activate = 4,
        End = 8,
        DurationEnd,
        Abort,
    }

    public enum SkillCastMode
    {
        Single = 1,
        Coordinate,
    }

    public class SkillArg
    {
        public Skill Skill { get; set; }
        public ushort Dir { get; set; }
        public Actors.ActorExt Caster { get; set; }
        public Actors.ActorExt Target { get; set; }
        public byte SkillSession;
        public int ApproachTime { get; set; }
        public SkillCastMode CastMode { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
        public short Z { get; set; }
        public int ActivationIndex { get; set; }
        public bool CastFinished { get; set; }
        
        List<SkillAffectedActor> affected = new List<SkillAffectedActor>();
        public List<SkillAffectedActor> AffectedActors { get { return affected; } }

        public SkillArg()
        {
            CastMode = SkillCastMode.Single;
        }
    }
}
