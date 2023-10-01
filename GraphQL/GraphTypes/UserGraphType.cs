using GraphQL.Types;
using Zerno.Models;
namespace Zerno.GraphQL.GraphTypes
{
    public class UserGraphType : ObjectGraphType<User>
    {
        public UserGraphType()
        {
            Name = "user";
        }
    }
}
