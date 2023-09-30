using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zerno.Models;

namespace Zerno.Data
{
    public class GrainContext : DbContext
    {
        public GrainContext(DbContextOptions<GrainContext> options) : base(options)
        {
            if(Users.Count() == 0)
            {
                try
                {
                    AddRange(users);
                    SaveChanges();
                    AddRange(products);
                    SaveChanges();
                    AddRange(requests);
                    SaveChanges();
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<User> Users { get; set; }

        private static readonly User[] users =
        {
            new User("Беспалова Варвара Александровна"),
            new User("Кириллова Диана Ярославовна"),
            new User("Жукова Камила Маратовна"),
            new User("Лукина Мария Егоровна"),
            new User("Власова Милана Данииловна"),
            new User("Комаров Ярослав Леонидович"),
            new User("Федорова Анна Тимофеевна"),
            new User("Соболева Милана Дмитриевна"),
            new User("Яковлева Полина Никитична"),
            new User("Кузьмин Никита Степанович"),
            new User("Медведева Кира Алексеевна"),
            new User("Смирнова Варвара Германовна"),
            new User("Смирнов Тимофей Артёмович"),
            new User("Карпова Надежда Адамовна")
        };
        private static readonly Product[] products =
        {
            new Product
            {
                Type = SeedType.Пшеница,
                StartDate = new DateOnly(2023, 12, 26),
                EndDate = new DateOnly(2024, 2, 4),
                DealerId = 2,
                Price = 100_000,
                FullAmmount = 10_000,
                Ammount = 10_000
            },
            new Product {
                Type = SeedType.Бобы,
                StartDate = new DateOnly(2023, 11, 15),
                EndDate = new DateOnly(2024, 1, 31),
                DealerId = 3,
                Price = 150_000,
                FullAmmount = 15_000,
                Ammount = 15_000
            },
            new Product {
                Type = SeedType.Кукуруза,
                StartDate = new DateOnly(2023, 12, 1),
                EndDate = new DateOnly(2024, 2, 10),
                DealerId = 1,
                Price = 120_000,
                FullAmmount = 12_000,
                Ammount = 12_000
            },
            new Product {
                Type = SeedType.Соя,
                StartDate = new DateOnly(2023, 11, 10),
                EndDate = new DateOnly(2024, 1, 20),
                DealerId = 4,
                Price = 90_000,
                FullAmmount = 9_000,
                Ammount = 9_000
            },
            new Product {
                Type = SeedType.Люпин,
                StartDate = new DateOnly(2023, 11, 5),
                EndDate = new DateOnly(2024, 2, 15),
                DealerId = 5,
                Price = 80_000,
                FullAmmount = 8_000,
                Ammount = 8_000
            }
        };
        private static readonly Request[] requests =
        {
            new Request
            {
                Ammount = 200,
                ProductId = 7,
                WanterId = 3,
                Date = new DateOnly(2024, 1, 1)
            },
            new Request{Ammount = 100, ProductId = 4, WanterId = 2, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 150, ProductId = 9, WanterId = 5, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 50, ProductId = 10, WanterId = 7, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 80, ProductId = 3, WanterId = 1, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 120, ProductId = 8, WanterId = 4, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 70, ProductId = 6, WanterId = 2, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 90, ProductId = 5, WanterId = 1, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 110, ProductId = 10, WanterId = 3, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 30, ProductId = 1, WanterId = 6, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 60, ProductId = 7, WanterId = 5, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 140, ProductId = 2, WanterId = 1, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 180, ProductId = 9, WanterId = 4, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 40, ProductId = 3, WanterId = 2, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 130, ProductId = 8, WanterId = 3, Date = new DateOnly(2024, 1, 1)},
            new Request{Ammount = 160, ProductId = 6, WanterId = 5, Date = new DateOnly(2024, 1, 1)},
        };
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateOnly>().HaveConversion<DateOnlyToStringConverter>();
        }
    }
}