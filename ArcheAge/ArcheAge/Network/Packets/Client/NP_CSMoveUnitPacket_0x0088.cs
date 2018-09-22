using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_CSMoveUnitPacket_0x0088 : NetPacket
    {
        public NP_CSMoveUnitPacket_0x0088(ClientConnection net) : base(01, 0x0088)
        {
            //1.0.1406
            //NP_CSMoveUnitPacket_0x0088
            //                     bc     type time     flags ix      iy      iz     vel.x vel.y vel.z rot.x rot.y rot.z a.dm.x a.dm.y a.dm.z a.stance a.alertness a.flags
            //Recv: 2500 0001 8800 F52700 01   86C20100 04    322B7A  F56D74  7DEB03 0000  0000  0000  00    00    2D    00     00     00     01       00          04
            //                                                8006450 7630325 256893
            /*
             * - <packet id="0x008801" name="CSMoveUnitPacket">
             *  <part name="bc" type="b" size="3" /> 
             *  <part name="type" type="c" id="1" /> 
             *  <part name="time" type="d" /> 
             *  <part name="flags" type="c" id="2" /> 
             *  - <bitwise_switch id="2">
             *  - <case id="0x10">
             *  <part name="scType" type="d" /> 
             *  <part name="phase" type="c" /> 
             *  </case>
             *  <case id="default" /> 
             *  </bitwise_switch>
             *  - <switch id="1">
             *  - <case id="1">
             *  <part name="ix" type="d3" /> 
             *  <part name="iy" type="d3" /> 
             *  <part name="iz" type="d3" /> 
             *  <part name="vel.x" type="h" /> 
             *  <part name="vel.y" type="h" /> 
             *  <part name="vel.z" type="h" /> 
             *  <part name="rot.x" type="C" /> 
             *  <part name="rot.y" type="C" /> 
             *  <part name="rot.z" type="C" /> 
             *  <part name="actor.deltaMovement.x" type="C" /> 
             *  <part name="actor.deltaMovement.y" type="C" /> 
             *  <part name="actor.deltaMovement.z" type="C" /> 
             *  <part name="actor.stance" type="C" /> 
             *  <part name="actor.alertness" type="C" /> 
             *  <part name="actor.flags" type="C" /> 
             *  - <bitwise_switch id="2">
             *  - <case id="0x80">
             *  <part name="actor.fallVel" type="h" /> 
             *  </case>
             *  - <case id="0x20">
             *  <part name="actor.gcFlags" type="C" /> 
             *  <part name="actor.gcPartId" type="C" /> 
             *  <part name="ix" type="d3" /> 
             *  <part name="iy" type="d3" /> 
             *  <part name="iz" type="d3" /> 
             *  <part name="rot.x" type="C" /> 
             *  <part name="rot.y" type="C" /> 
             *  <part name="rot.z" type="C" /> 
             *  </case>
             *  - <case id="0x60">
             *  <part name="actor.gcId" type="d" /> 
             *  </case>
             *  - <case id="0x40">
             *  <part name="actor.climbData" type="d" /> 
             *  </case>
             *  <case id="default" /> 
             *  </bitwise_switch>
             *  </case>
             *  - <case id="2">
             *  <part name="ix" type="d3" /> 
             *  <part name="iy" type="d3" /> 
             *  <part name="iz" type="d3" /> 
             *  <part name="vel.x" type="h" /> 
             *  <part name="vel.y" type="h" /> 
             *  <part name="vel.z" type="h" /> 
             *  <part name="rot.x" type="h" /> 
             *  <part name="rot.y" type="h" /> 
             *  <part name="rot.z" type="h" /> 
             *  <part name="vehicle.angVel[0]" type="f" /> 
             *  <part name="vehicle.angVel[1]" type="f" /> 
             *  <part name="vehicle.angVel[2]" type="f" /> 
             *  <part name="vehicle.steering" type="C" /> 
             *  <part name="vehicle.wheelVelCount" type="C" /> 
             *  </case>
             *  - <case id="3">
             *  <part name="ix" type="d3" /> 
             *  <part name="iy" type="d3" /> 
             *  <part name="iz" type="d3" /> 
             *  <part name="vel.x" type="h" /> 
             *  <part name="vel.y" type="h" /> 
             *  <part name="vel.z" type="h" /> 
             *  <part name="rot.x" type="h" /> 
             *  <part name="rot.y" type="h" /> 
             *  <part name="rot.z" type="h" /> 
             *  <part name="ship.angVel[0]" type="f" /> 
             *  <part name="ship.angVel[1]" type="f" /> 
             *  <part name="ship.angVel[2]" type="f" /> 
             *  <part name="ship.steering" type="C" /> 
             *  <part name="ship.throttle" type="C" /> 
             *  <part name="ship.zoneId" type="h" /> 
             *  <part name="ship.stucked" type="C" /> 
             *  </case>
             *  - <case id="4">
             *  <part name="shipRequest.throttle" type="C" /> 
             *  <part name="shipRequest.steering" type="C" /> 
             *  </case>
             *  - <case id="5">
             *  <part name="ix" type="d3" /> 
             *  <part name="iy" type="d3" /> 
             *  <part name="iz" type="d3" /> 
             *  <part name="vel.x" type="h" /> 
             *  <part name="vel.y" type="h" /> 
             *  <part name="vel.z" type="h" /> 
             *  <part name="rot.x" type="h" /> 
             *  <part name="rot.y" type="h" /> 
             *  <part name="rot.z" type="h" /> 
             *  <part name="transfer.angVel[0]" type="f" /> 
             *  <part name="transfer.angVel[1]" type="f" /> 
             *  <part name="transfer.angVel[2]" type="f" /> 
             *  <part name="transfer.steering" type="d" /> 
             *  <part name="transfer.pathPointIndex" type="d" /> 
             *  <part name="transfer.speed" type="f" /> 
             *  <part name="transfer.reverse" type="C" /> 
             *  </case>
             *  - <case id="default">
             *  <part name="ix" type="d3" /> 
             *  <part name="iy" type="d3" /> 
             *  <part name="iz" type="d3" /> 
             *  <part name="vel.x" type="h" /> 
             *  <part name="vel.y" type="h" /> 
             *  <part name="vel.z" type="h" /> 
             *  <part name="rot.x" type="h" /> 
             *  <part name="rot.y" type="h" /> 
             *  <part name="rot.z" type="h" /> 
             *  </case>
             *  </switch>
             *  </packet>
             */
        }
    }
}