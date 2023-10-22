using System.ComponentModel.DataAnnotations;
using Zerno.Models;

namespace Zerno.DTOs
{
    public class ProductDTO
    {
        public SeedType Type { get; set; }
        public ulong FullAmmount { get; set; }
        public ulong Ammount { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public int DealerId { get; set; }
    }
}
