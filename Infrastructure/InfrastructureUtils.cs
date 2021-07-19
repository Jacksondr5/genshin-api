using MongoDB.Driver;
using System.Threading.Tasks;

namespace Infrastructure
{
    internal abstract class QueryResult
    {
        public string Id { get; set; } = "";
    }
    internal class MaxQueryResult : QueryResult
    {
        public int Max { get; set; }
    }

    internal static class InfrastructureUtils
    {
        public static async Task<int?> GetMaxId<T>(
            IMongoCollection<T> collection
        )
        {
            var query =
                "{ $group: { _id: \"MaxQueryResult\", Max: { $max: \"$_id\" } } } ";
            var queryResult = await collection
                .Aggregate()
                .AppendStage<MaxQueryResult>(query)
                .FirstOrDefaultAsync();
            return queryResult?.Max;
        }
    }
}