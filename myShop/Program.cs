using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop
{
    enum Status
    {
        Bonus = 9, //blye
        Event = 12, //red
        HighRating = 10, //freen
        Normal = 15
    }
    struct Product
    {
        public int Id;
        public string Name;
        public string Description;
        public int Price;
        public Status Status;
        public List<Product> RelatedProducts;
    }
    //struct WarHouseProduct
    //{
    //    public Product Product;
    //    public int Count;
    //}
    //struct ProductInBasket
    //{
    //    public Product Product;
    //    public int Count;
    //}
    class Program
    {
        static string FILE_PATH = "ListOfProducts.txt";
        static List<Product> _products;
        static void Main(string[] args)
        {
            _products = new List<Product>();

            //Status status = (Status)"normal";

            fillListOfProducts();
            saveListOfProducts();
            loadListOfProducts();
            


            Console.ReadLine();
        }
        static List<Product> loadListOfProducts()
        {
            List<Product> products = new List<Product>();

            string listOfProductsText = File.ReadAllText(FILE_PATH);

            string[] productsWithRelated = listOfProductsText.Split(new string[] { "***\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string productWithRelated in productsWithRelated)
            {
                string[] productProperties = productWithRelated.Split(new string[] { "|\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                Product product = new Product()
                {
                    Id = Convert.ToInt32(productProperties[0]),
                    Name = productProperties[1],
                    Description = productProperties[2],
                    Price = Convert.ToInt32(productProperties[3]),
                    Status = (Status)Convert.ToInt32(productProperties[4]),
                    RelatedProducts = new List<Product>()
                };
                string[] relatedProducts = productProperties[5].Split(new string[] { "\t+++" }, StringSplitOptions.RemoveEmptyEntries);

            }


            return products;


        }
        static void saveListOfProducts()
        {
            string textListOfProducts = "";
            foreach (Product product in _products)
            {
                textListOfProducts += $"{product.Id}|\r\n{product.Name}|\r\n{product.Description}|\r\n{product.Price}|\r\n{product.Status}|\r\n";
                string textRelatedProducts = "";
                foreach (Product relatedProduct in product.RelatedProducts)
                {
                    textRelatedProducts += $"\t+++\r\n\t{relatedProduct.Id}\r\n\t{relatedProduct.Name}\r\n\t{relatedProduct.Description}\r\n\t{relatedProduct.Price}\r\n\t{relatedProduct.Status}\r\n";
                }
                textListOfProducts += textRelatedProducts + "***\r\n";
            }
            File.WriteAllText(FILE_PATH, textListOfProducts);
        }
        static void fillListOfProducts()
        {
            _products.Add(new Product()
            {
                Id = 27,
                Name = "Телевизор",
                Description = "Модель L100 имеет медиацентр, позволяющий воспроизводить любой контент с USB-накопителя.",
                Price = 20000,
                Status = Status.Normal,
                RelatedProducts = new List<Product>()
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
                        Status = Status.Normal
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
                RelatedProducts = new List<Product>()
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
                RelatedProducts = new List<Product>()
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
                        Status = Status.Normal
                    },
                }
            });
        }
    }
}
