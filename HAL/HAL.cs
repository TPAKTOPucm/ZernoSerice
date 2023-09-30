using System.ComponentModel;
using System.Dynamic;
using System.Text.Json.Serialization;
using Zerno.Models;

namespace Zerno
{
    public static class HAL
    {
        public static dynamic ToResource(this Product product)
        {
            var resource = product.ToDynamic();
            resource._links = new
            {
                self = new { href = $"/api/product/{product.Id}" },
                dealer = new { href = $"/api/user/{product.DealerId}" },
                requests = new { href = $"/api/request/getByProductId?productId={product.Id}" }
            };
            return resource;
        }

        public static dynamic ToResource(this Request request)
        {
            var resource = request.ToDynamic();
            resource._links = new
            {
                self = new { href = $"/api/request/{request.Id}" },
                wanter = new { href = $"/api/user/{request.WanterId}" },
                product = new { href = $"/api/product/{request.ProductId}" }
            };
            return resource;
        }

        public static dynamic ToResource(this User user)
        {
            var resource = user.ToDynamic();
            resource._links = new
            {
                self = new { href = $"/api/user/{user.Id}" },
                //dealer = new { href = $"/api/user/{user.DealerId}" },
                //requests = new { href = $"/api/request/getByProductId?productId={user.Id}" }
            };
            return resource;
        }

        public static dynamic ToDynamic(this object value)
        {
            var result = new ExpandoObject();
            var properties = TypeDescriptor.GetProperties(value.GetType());
            foreach (PropertyDescriptor property in properties )
            {
                if(!Ignore(property))
                    result.TryAdd(property.Name, property.GetValue(value));
            }
            return result;
        }

        private static bool Ignore(this PropertyDescriptor property)
        {
            return property.Attributes.OfType<JsonIgnoreAttribute>().Any();
        }

        public static dynamic Paginate(string baseUrl, int index, int count, int total)
        {
            dynamic links = new ExpandoObject();
            links.self = new { href = baseUrl };
            if (index + count < total)
            {
                links.final = new { href = $"{baseUrl}?index={total - (total % count)}&count={count}" };
                links.next = new { href = $"{baseUrl}?index={index + count}" };
            }
            if (index > 0)
            {
                links.first = new { href = $"{baseUrl}?index=0" };
                links.prev = new { href = $"{baseUrl}?index={index - count}" };
            }
            return links;
        }

    }
}
