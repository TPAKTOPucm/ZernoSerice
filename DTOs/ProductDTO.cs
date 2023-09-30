using Zerno.Models;

namespace Zerno.DTOs
{
    public class ProductDTO
    {
        public int? Id { get; set; }
        public SeedType Type { get; set; }
        public ulong FullAmmount { get; set; }
        public ulong Ammount { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal Price { get; set; }
        public int DealerId { get; set; }
    }
}
