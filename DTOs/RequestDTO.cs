using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Zerno.Models;

namespace Zerno.DTOs
{
    public class RequestDTO
    {
        public int? Id { get; set; }
        public ulong Ammount { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public int WanterId { get; set; }
    }
}
