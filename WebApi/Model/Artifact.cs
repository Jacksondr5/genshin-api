using System.Collections.Generic;

namespace WebApi.Model
{
    public class Artifact
    {
        public int Id { get; set; }
        public ArtifactStat MainStat { get; set; } = new ArtifactStat();
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