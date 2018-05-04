using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Skills
{
    public class Skill
    {
        SkillData baseData;
        public SkillData BaseData { get { return baseData; } }
        public uint ID { get { return baseData.ID; } }
        public bool Dummy { get; set; }
        public DateTime CoolDownEndTime { get; set; }

        public Skill(SkillData data)
        {
            baseData = data;
            CoolDownEndTime = DateTime.Now;
        }

        public override string ToString()
        {
            return baseData.Name;
        }
    }
}
