using System.Collections.Generic;

namespace Core
{
    public class Artifact
    {
        public int Id { get; set; }
        public ArtifactStat MainStat { get; set; } = new ArtifactStat();
        public int Set { get; set; }
        public List<ArtifactStat> SubStats { get; set; } =
            new List<ArtifactStat>();
        public int Type { get; set; }
    }

    public class ArtifactStat
    {
        public int StatType { get; set; }
        public int StatName { get; set; }
        public float Value { get; set; }
    }
}