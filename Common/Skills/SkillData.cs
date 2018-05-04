using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Skills
{
    public enum SkillCastStances
    {
        None,
        Down,
        TakeDown,
        NoMove,
    }

    public enum NoTargetTypes
    {
        /// <summary>
        /// 线性
        /// </summary>
        Linear,
        /// <summary>
        /// 角度扇形
        /// </summary>
        Angular,
    }

    public class SkillData
    {
        List<uint> related = new List<uint>();
        List<uint> previous = new List<uint>();
        List<int> activationTimes = new List<int>();

        public uint Effect { get; set; }
        /// <summary>
        /// 技能ID
        /// </summary>
        public uint ID { get; set; }
        /// <summary>
        /// 技能名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 技能释放类型
        /// </summary>
        public SkillType SkillType { get; set; }
        /// <summary>
        /// 最小攻击系数
        /// </summary>
        public float MinAtk { get; set; }
        /// <summary>
        /// 最大攻击系数
        /// </summary>
        public float MaxAtk { get; set; }
        /// <summary>
        /// 最小攻击范围（1/10 m）
        /// </summary>
        public int CastRangeMin { get; set; }
        /// <summary>
        /// 最大攻击范围（1/10 m）
        /// </summary>
        public int CastRangeMax { get; set; }
        /// <summary>
        /// MP消耗，意义根据职业不同而不同
        /// </summary>
        public int ManaCost { get; set; }
        /// <summary>
        /// 冷却时间(ms)
        /// </summary>
        public int CoolDown { get; set; }
        /// <summary>
        /// 吟唱时间(ms)
        /// </summary>
        public int CastTime { get; set; }

        /// <summary>
        /// 技能发动时动作所需时间
        /// </summary>
        public int ActionTime { get; set; }

        /// <summary>
        /// 技能发动后的硬直时间
        /// </summary>
        public List<int> ActivationTimes { get { return activationTimes; } }

        /// <summary>
        /// 技能效果持续时间
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 技能效果是否需要飞行时间
        /// </summary>
        public bool ShouldApproach { get; set; }

        /// <summary>
        /// 技能的接近速度
        /// </summary>
        public int ApproachTimeRate { get; set; }

        /// <summary>
        /// 无目标技能类型
        /// </summary>
        public NoTargetTypes NoTargetType { get; set; }

        /// <summary>
        /// 无目标技能角度范围
        /// </summary>
        public int NoTargetAngle { get; set; }

        /// <summary>
        /// 无目标技能的攻击范围
        /// </summary>
        public int NoTargetRange { get; set; }

        /// <summary>
        /// 无目标宽度
        /// </summary>
        public int NoTargetWidth { get; set; }

        /// <summary>
        /// 相关技能，一般来说是各种不同情况下使用该技能所施展的子技能ID
        /// </summary>
        public List<uint> RelatedSkills { get { return related; } }

        /// <summary>
        /// 施展该技能所需的前置技能
        /// </summary>
        public List<uint> PreviousSkills { get { return previous; } }

        /// <summary>
        /// 目标所需要的姿态
        /// </summary>
        public SkillCastStances RequiredTargetStance { get; set; }
        /// <summary>
        /// 技能释放者需要的姿态
        /// </summary>
        public SkillCastStances RequiredCasterStance { get; set; }

        /// <summary>
        /// 可以使攻击力提升的Buff名称
        /// </summary>
        public string BonusAddition { get; set; }

        /// <summary>
        /// 单个Buff攻击力提升量
        /// </summary>
        public float BonusRate { get; set; }

        /// <summary>
        /// 在技能施放时动作锁定
        /// </summary>
        public int MovementLockOnCasting { get; set; }

        /// <summary>
        /// 在技能发动时动作锁定
        /// </summary>
        public int MovementLockOnAction { get; set; }

        public SkillData()
        {
            NoTargetAngle = 45;
            NoTargetWidth = 10;
            ApproachTimeRate = 3;
            NoTargetType = NoTargetTypes.Angular;
        }
    }
}
