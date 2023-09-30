using System.Text.Json.Serialization;

namespace Zerno.Models
{
    public class User
    {
        public User()
        {
            Products = new HashSet<Product>();
            BuyRequests = new HashSet<Request>();
        }
        public User(string fio) : this() 
        {
            var tmp = fio.Split(' ');
            FirstName = tmp[0];
            LastName = tmp[1];
            MiddleName = tmp[2];
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
        [JsonIgnore]
        public ICollection<Request> BuyRequests { get; set; }
    }
}
