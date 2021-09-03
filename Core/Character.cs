using System.Collections.Generic;

namespace Core
{
    public class Character : StorableData
    {
        public List<Loadout> Loadouts { get; set; } = new();
        public string Name { get; set; } = "";
    }

    public class Loadout : StorableData
    {
        public int CircletId { get; set; }
        public int ClockId { get; set; }
        public int CupId { get; set; }
        public int FeatherId { get; set; }
        public int FlowerId { get; set; }
        public string Name { get; set; } = "";
    }
}