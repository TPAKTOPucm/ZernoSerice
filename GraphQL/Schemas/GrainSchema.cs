using GraphQL.Types;
using Zerno.GraphQL.Mutations;
using Zerno.GraphQL.Queries;
using Zerno.Services;

namespace Zerno.GraphQL.Schemas
{
    public class GrainSchema : Schema
    {
        public GrainSchema(IGrainStorage db)
        {
            Query = new GrainQuery(db);
            Mutation = new GrainMutation(db);
        }
    }
}
