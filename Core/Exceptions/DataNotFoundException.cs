namespace Core.Exceptions
{
    public class DataNotfoundException<T>
        : GenshinUserException
        where T : StorableData
    {
        internal int Id { get; set; }
        public DataNotfoundException(int id) : base(GetMessage(id))
        {
            Id = id;
        }

        private static string GetMessage(int id)
        {
            var objectType = typeof(T);
            var beginning = typeof(T).Name switch
            {
                "Artifact" => "An artifact",
                "Character" => "A character",
                "Loadout" => "A loadout",
                "Team" => "A team",
                "TestStorableData" => "A test storable data",
                _ => throw new GenshinApplicationException(
                    "type used in DataNotFoundException doesn't correspond to a known StorableData (**this shouldn't happen due to generic constraints**)"
                ),
            };
            return $"{beginning} with id {id} could not be found";
        }
    }
}