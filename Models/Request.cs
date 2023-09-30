using System.Text.Json.Serialization;

namespace Zerno.Models
{
    public class Request
    {
        public int Id { get; set; }
        public ulong Ammount { get; set; }
        public DateOnly Date { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public int WanterId { get; set; }
        [JsonIgnore]
        public User Wanter { get; set; }
    }
}
