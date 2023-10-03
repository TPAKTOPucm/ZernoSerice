using GraphQL;
using GraphQL.Execution;
using GraphQL.Types;
using Zerno.Data;
using Zerno.GraphQL.GraphTypes;
using Zerno.Models;

namespace Zerno.GraphQL.Queries
{
    public class GrainQuery : ObjectGraphType
    {
        private readonly IGrainStorage _db;
        public GrainQuery(IGrainStorage db)
        {
            _db = db;
            Field<ListGraphType<ProductGraphType>>("Products", "Return all products", resolve: GetAllProducts);
            Field<ProductGraphType>("Product", "Return product by id", new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }), resolve: GetProduct);

            Field<ListGraphType<UserGraphType>>("Users", "Return all users", resolve: GetAllUsers);
            Field<UserGraphType>("User", "Return user by id", new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }), resolve: GetUser);

            Field<ListGraphType<RequestGraphType>>("Requests", "Return all requests by product or wanter id or return all requests if query has no ids", new QueryArguments(
                new QueryArgument<IntGraphType> { Name = "productId"},
                new QueryArgument<IntGraphType> { Name = "userId"}
                ),resolve: GetRequests);
            Field<RequestGraphType>("Request", "Return request by id", new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }), resolve: GetRequest);
        }

        private ICollection<Product> GetAllProducts(IResolveFieldContext _) => _db.GetProducts();
        private Product GetProduct(IResolveFieldContext<object> context) => _db.GetProductById(context.GetArgument<int>("id"));

        private ICollection<User> GetAllUsers(IResolveFieldContext _) => _db.GetUsers();
        private User GetUser(IResolveFieldContext<object> context) => _db.GetUserById(context.GetArgument<int>("id"));
        private Request GetRequest(IResolveFieldContext<object> context) => _db.GetRequestById(context.GetArgument<int>("id"));
        private ICollection<Request> GetRequests(IResolveFieldContext<object> context)
        {
            ArgumentValue value;
            if (context.Arguments.TryGetValue("productId", out value))
                return _db.GetRequestsByProductId((int)value.Value);
            if (context.Arguments.TryGetValue("userId", out value))
                return _db.GetRequestsByUserId((int)value.Value);
            return _db.GetRequests();
        }
    }
}
