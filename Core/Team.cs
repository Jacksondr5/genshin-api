namespace Core
{
    public class Team : StorableData
    {
        public int Character1Id { get; set; }
        public int Character2Id { get; set; }
        public int Character3Id { get; set; }
        public int Character4Id { get; set; }
        public int Loadout1Id { get; set; }
        public int Loadout2Id { get; set; }
        public int Loadout3Id { get; set; }
        public int Loadout4Id { get; set; }
        public string Name { get; set; } = "";
    }
}