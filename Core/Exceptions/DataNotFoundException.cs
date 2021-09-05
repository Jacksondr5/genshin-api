using System;
using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class DataNotFoundException<T>
        : GenshinUserException
        where T : StorableData
    {
        internal int Id { get; set; }
        public DataNotFoundException(int id) : base(GetMessage(id))
        {
            Id = id;
        }

        protected DataNotFoundException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }

        private static string GetMessage(int id)
        {
            var beginning = typeof(T).Name switch
            {
                "Artifact" => "An artifact",
                "Character" => "A character",
                "Loadout" => "A loadout",
                "Team" => "A team",
                "TestStorableData" => "A test storable data",
                _ => throw new GenshinApplicationException(UnknownType),
            };
            return $"{beginning} with id {id} could not be found";
        }

        public const string UnknownType =
            "The type used in DataNotFoundException doesn't correspond to a known StorableData (**this shouldn't happen due to generic constraints**)";
    }
}