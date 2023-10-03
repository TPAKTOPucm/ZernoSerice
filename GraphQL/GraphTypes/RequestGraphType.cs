using GraphQL.Types;
using Zerno.Models;

namespace Zerno.GraphQL.GraphTypes
{
    public class RequestGraphType : ObjectGraphType<Request>
    {
        public RequestGraphType() 
        {
            Name = "request";
            Field(r => r.Wanter, type: typeof(UserGraphType)).Description("Пользователя, предложивший сделку");
            Field(r => r.Product, type: typeof(ProductGraphType));
            Field(r => r.Date).Description("Дата сделки");
            Field(r => r.Ammount).Description("Желаемое количество продукции(кг)");
        }
    }
}
