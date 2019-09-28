using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop
{
    class Program
    {
        enum Status
        {
            Bonus = 9, //blye
            Event = 12, //red
            HighRating = 10, //freen
            None = 15
        }
        struct Product
        {
            public int Id;
            public string Name;
            public string Description;
            public int Price;
            public Status Status;
            public List<Product> SubProducts;
        }
        struct WarHouseProduct
        {
            public Product Product;
            public int Count;
        }
        struct ProductInBasket
        {
            public Product Product;
            public int Count;
        }
        static List<Product> _products;
        static void Main(string[] args)
        {

        }
        static void fillListOfProducts()
        {
            _products.Add(new Product()
            {
                Id = 27,
                Name = "Телевизор",
                Description = "Модель L100 имеет медиацентр, позволяющий воспроизводить любой контент с USB-накопителя.",
                Price = 20000,
                Status = Status.None,
                SubProducts = new List<Product>()
                {
                    new Product()
                    {
                        Id = 73,
                        Name = "Наушники",
                        Description = "Серьезным преимуществом станет отсутствие кабелей.",
                        Price = 4500,
                        Status = Status.HighRating
                    },
                    new Product()
                    {
                        Id = 52,
                        Name = "Стабилизатор напряжения",
                        Description = "Для подключения устройств и электроприборов техника оборудована тремя евророзетками.",
                        Price = 5000,
                        Status = Status.None
                    },
                    new Product()
                    {
                        Id = 84,
                        Name = "Салфетка",
                        Description = "Универсальная салфетка Magic создана для очистки абсолютно всего.",
                        Price = 300,
                        Status = Status.Event
                    },
                }
            });

            _products.Add(new Product()
            {
                Id = 48,
                Name = "Холодильник",
                Description = "Для компактного хранения большого количества продуктов питания.",
                Price = 15000,
                Status = Status.Bonus,
                SubProducts = new List<Product>()
                {
                    new Product()
                    {
                        Id = 21,
                        Name = "Набор для уборки",
                        Description = "Средство для очистки активно воздействует на глубокие следы от жира, остатков пищи или грязи.",
                        Price = 2000,
                        Status = Status.HighRating
                    },
                    new Product()
                    {
                        Id = 59,
                        Name = "Набор контейнеров",
                        Description = "Изделия изготовлены из прозрачного пластика.",
                        Price = 999,
                        Status = Status.Bonus
                    },
                }
            });

            _products.Add(new Product()
            {
                Id = 19,
                Name = "Игровая консоль",
                Description = "Игровая приставка это тысячи часов, проведенных за прохождением интереснейших видеоигр.",
                Price = 33000,
                Status = Status.HighRating,
                SubProducts = new List<Product>()
                {
                    new Product()
                    {
                        Id = 70,
                        Name = "Диск с игрой",
                        Description = "Вас ждет максимально проработанный игровой мир, предоставляющий вам огромные возможности для выживания.",
                        Price = 4199,
                        Status = Status.Event
                    },
                    new Product()
                    {
                        Id = 40,
                        Name = "Геймпад",
                        Description = "отальный контроль за процессом, максимум удовольствия от точности реакций.",
                        Price = 3000,
                        Status = Status.None
                    },
                }
            });
        }

    }
}
