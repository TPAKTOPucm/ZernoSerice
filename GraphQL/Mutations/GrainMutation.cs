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
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "startDate" },
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "endDate" },
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
                        DealerId = context.GetArgument<int>("dealerId"),
                        Type = Enum.Parse<SeedType>(context.GetArgument<int>("seedTypeCode").ToString())
                    };
                    product.Dealer = _db.GetUserById(product.DealerId);
                    _db.CreateProduct(product);
                    return product;
                }
                );
            Field<UserGraphType>("createUser", arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "firstname" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "lastname" },
                    new QueryArgument<StringGraphType> { Name = "middlename" }
                ),
                resolve: context =>
                {
                    var user = new User()
                    {
                        FirstName = context.GetArgument<string>("firstname"),
                        LastName = context.GetArgument<string>("lastname"),
                        MiddleName = context.GetArgument<string>("middlename")
                    };
                    _db.CreateUser(user);
                    return user;
                }
                );
            Field<RequestGraphType>("createRequest", arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "productId" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "wanterId" },
                    new QueryArgument<NonNullGraphType<ULongGraphType>> { Name = "ammount" },
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "date" }
                ),
                resolve: context =>
                {
                    var request = new Request()
                    {
                        ProductId = context.GetArgument<int>("productId"),
                        WanterId = context.GetArgument<int>("wanterId"),
                        Ammount = context.GetArgument<ulong>("ammount"),
                        Date = context.GetArgument<DateTime>("date")
                    };
                    _db.CreateRequest(request);
                    request = _db.GetRequestById(request.Id);
                    return request;
                });
        }
    }
}
