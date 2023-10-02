using GraphQL;
using GraphQL.Types;
using Zerno.Data;
using Zerno.GraphQL.GraphTypes;
using Zerno.Models;

namespace Zerno.GraphQL.Mutations
{
    public class GrainMutation : ObjectGraphType
    {
        private readonly IGrainStorage _db;
        public GrainMutation(IGrainStorage db) 
        { 
            _db = db;

            Field<ProductGraphType>("createProduct", arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ULongGraphType>> { Name = "ammount" },
                    new QueryArgument<NonNullGraphType<DateGraphType>> { Name = "startDate" },
                    new QueryArgument<NonNullGraphType<DateGraphType>> { Name = "endDate" },
                    new QueryArgument<NonNullGraphType<DecimalGraphType>> { Name = "price" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "dealerId" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "seedTypeCode" }
                ),
                resolve: context =>
                {
                    var ammount = context.GetArgument<ulong>("ammount");
                    var product = new Product()
                    {
                        FullAmmount = ammount,
                        Ammount = ammount,
                        Price = context.GetArgument<decimal>("price"),
                        StartDate = context.GetArgument<DateTime>("startDate"),
                        EndDate = context.GetArgument<DateTime>("endDaate"),
                        DealerId = context.GetArgument<int>("dealeerId"),
                        Type = Enum.Parse<SeedType>(context.GetArgument<string>("seedTypeCode"))
                    };
                    _db.CreateProduct(product);
                    return product;
                }
                );
        }
    }
}
