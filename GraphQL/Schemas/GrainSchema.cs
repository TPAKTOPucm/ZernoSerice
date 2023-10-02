﻿using GraphQL.Types;
using Zerno.Data;
using Zerno.GraphQL.Mutations;
using Zerno.GraphQL.Queries;

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
