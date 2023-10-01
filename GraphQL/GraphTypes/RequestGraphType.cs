using GraphQL.Types;
using Zerno.Models;

namespace Zerno.GraphQL.GraphTypes
{
    public class RequestGraphType : ObjectGraphType<Request>
    {
        public RequestGraphType() 
        {
            Name = "reuest";
            //Field(r => r.WanterId).Description("Идентификатор пользователя, предложившего сделку");
            Field(r => r.Date).Description("Дата сделки");
            Field(r => r.Ammount).Description("Желаемое количество продукции(кг)");
        }
    }
}
