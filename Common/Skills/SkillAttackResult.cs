using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Skills
{
    public enum SkillAttackResult
    {
        Normal,
        Miss,
        Avoid,
        Parry,
        TotalParry,
        TotalParrySkill,
        Counter = 6,
        Critical = 7,
    }
}
