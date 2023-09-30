using System.Text.Json.Serialization;

namespace Zerno.Models
{
    public enum SeedType
    {
        Пшеница = 0,
        Зерно = 1,
        Бобы = 2,
        Горох = 3,
        Гречиха = 4,
        Кукуруза = 5,
        Люпин = 6,
        Нут = 7,
        Овёс = 8,
        Полба = 9,
        Просо = 10,
        Ячмень = 11,
        Рожь = 12,
        Сорго = 13,
        Соя = 14,
        Тритикале = 15,
        Фасоль = 16,
        Чечевица = 17
    }
    public class Product
    {
        public int? Id { get; set; }
        public SeedType Type { get; set; }
        public ulong FullAmmount { get; set; }
        public ulong Ammount { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal Price { get; set; }
        public int DealerId { get; set; }
        [JsonIgnore]
        public User Dealer { get; set; }
        [JsonIgnore]
        public ICollection<Request> Requests { get; set; }
    }
}
