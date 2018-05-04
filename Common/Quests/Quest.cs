using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Quests
{
    public class Quest
    {
        public const int Counts = 5;
        int[] count = new int[Counts];
        public ushort QuestID { get; set; }
        public byte Step { get; set; }
        public byte StepStatus { get; set; }
        public byte NextStep { get; set; }
        public short Flag1 { get; set; }
        public short Flag2 { get; set; }
        public short Flag3 { get; set; }
        public int[] Count { get { return count; } }
    }
}
