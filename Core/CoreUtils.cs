using Core.Exceptions;

namespace Core
{
    //TODO: figure out how to use this for both exceptions and mongo
    public static class CoreUtils
    {
        public static string GetStorableDataName<T>()
        {
            var type = typeof(T);
            if (type.GUID == typeof(Artifact).GUID)
            {
                return "artifacts";
            }
            else if (type.GUID == typeof(Character).GUID)
            {
                return "characters";
            }
            else if (type.GUID == typeof(Team).GUID)
            {
                return "teams";
            }
            else
            {
                throw new GenshinApplicationException(
                    "type used in GenericRepository doesn't correspond to a known mongo db collection"
                );
            }
        }
    }
}