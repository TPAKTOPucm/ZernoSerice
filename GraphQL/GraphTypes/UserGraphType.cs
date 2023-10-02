using GraphQL.Types;
using Zerno.Models;
namespace Zerno.GraphQL.GraphTypes
{
    public class UserGraphType : ObjectGraphType<User>
    {
        public UserGraphType()
        {
            Name = "user";
            Field(u => u.FirstName);
            Field(u => u.LastName);
            Field(u => u.MiddleName);
        }
    }
}
