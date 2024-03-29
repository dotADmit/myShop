﻿using System;
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
    struct ProductInWarhouse
    {
        public Product Product;
        public int Count;
    }
    //struct ProductInBasket
    //{
    //    public Product Product;
    //    public int Count;
    //}
    class Program
    {
        const string FILE_PATH = "ListOfProducts.txt";
        const int ID_COLUMN_WIDTH = 4;
        const int NAME_COLUMN_WIDTH = 20;
        const int DETAILS_COLUMN_WIDTH = 35;
        const int PRICE_COLUMN_WIDTH = 12;
        const int WAREHOUSE_COLUMN_WIDTH = 8;


        static List<ProductInWarhouse> _warehouse;
        static List<Product> _products;
        static void Main(string[] args)
        {
            init();
            printTitleView("- - - - - С П И С О К   Т О В А Р О В - - - - -", ID_COLUMN_WIDTH + NAME_COLUMN_WIDTH + DETAILS_COLUMN_WIDTH + PRICE_COLUMN_WIDTH + WAREHOUSE_COLUMN_WIDTH + 17);
            printTableView();

            printRowTextView(77, "sdfsf", "jfjkdifdfd", 9890, 978, ConsoleColor.Red);


            Console.ReadLine();
        }
        static void init()
        {
            if (!File.Exists(FILE_PATH))
            {
                _products = new List<Product>();
                fillListOfProducts();
                saveListOfProducts();
            }
            else
            {
                _products = loadListOfProducts();
                if (_products == null)
                {
                    Console.WriteLine("Данные не загружены");
                    Console.ReadKey();
                    return;
                }
                if (_products.Count == 0)
                {
                    fillListOfProducts();
                    saveListOfProducts();
                }
            }
        }
        static void printTitleView(string title, int width)
        {
            Console.WriteLine();

            int spaceCount = (width - title.Length) / 2;
            string displayTitle = new string(' ', spaceCount) + title;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(displayTitle);
            Console.ResetColor();

            Console.WriteLine();
        }
        static void printTableView()
        {
            string title = $"| {"Id",ID_COLUMN_WIDTH} | {"Наименование",NAME_COLUMN_WIDTH} | {"Описание",DETAILS_COLUMN_WIDTH} | {"Цена",PRICE_COLUMN_WIDTH} | {"Остаток",WAREHOUSE_COLUMN_WIDTH} |";
            string lineHeader = $"+{new string('-', ID_COLUMN_WIDTH + 2)}+{new string('-', NAME_COLUMN_WIDTH + 2)}+{new string('-', DETAILS_COLUMN_WIDTH + 2)}+{new string('-', PRICE_COLUMN_WIDTH + 2)}+{new string('-', WAREHOUSE_COLUMN_WIDTH + 2)}+";
            string lineRow = lineHeader.Replace("+", "|");

            Console.WriteLine(lineHeader);
            Console.WriteLine(title);
            Console.WriteLine(lineRow);

        }
        static void printProductView()
        {

        }
        static void printEntryView()
        {

        }
        static void printRowTextView(int id, string name, string details, int price, int warehouse, ConsoleColor color)
        {
            Console.ResetColor();
            Console.Write("| ");
            Console.ForegroundColor = color;
            Console.Write($"{id,ID_COLUMN_WIDTH}");
            printDashSetandResetColor(color);
            Console.Write($"{name,NAME_COLUMN_WIDTH * -1}");
            printDashSetandResetColor(color);
            Console.Write($"{details,DETAILS_COLUMN_WIDTH * -1}");
            printDashSetandResetColor(color);
            Console.Write($"{price,PRICE_COLUMN_WIDTH - 5} руб.");
            printDashSetandResetColor(color);
            Console.Write($"{warehouse,WAREHOUSE_COLUMN_WIDTH - 4} шт.");
            Console.ResetColor();
            Console.Write(" |");
            Console.ForegroundColor = color;
        }

        static void printHelpView()
        {

        }
        static string getCommandView()
        {
            return "";
        }

        static List<Product> loadListOfProducts()
        {
            List<Product> products = new List<Product>();

            string listOfProductsText = File.ReadAllText(FILE_PATH);

            if (listOfProductsText == "")
                return null;

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
                foreach (string relatedItem in relatedProducts)
                {
                    string[] relatedItemProperties = relatedItem.Split(new char[] { '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    Product relatedProduct = new Product()
                    {
                        Id = Convert.ToInt32(relatedItemProperties[0]),
                        Name = relatedItemProperties[1],
                        Description = relatedItemProperties[2],
                        Price = Convert.ToInt32(relatedItemProperties[3]),
                        Status = (Status)Convert.ToInt32(relatedItemProperties[4]),
                    };
                    product.RelatedProducts.Add(relatedProduct);
                }
                products.Add(product);
            }
            return products;
        }
        static void saveListOfProducts()
        {
            string textListOfProducts = "";
            foreach (Product product in _products)
            {
                textListOfProducts += $"{product.Id}|\r\n{product.Name}|\r\n{product.Description}|\r\n{product.Price}|\r\n{(int)product.Status}|\r\n";
                string textRelatedProducts = "";
                foreach (Product relatedProduct in product.RelatedProducts)
                {
                    textRelatedProducts += $"\t+++\r\n\t{relatedProduct.Id}\r\n\t{relatedProduct.Name}\r\n\t{relatedProduct.Description}\r\n\t{relatedProduct.Price}\r\n\t{(int)relatedProduct.Status}\r\n";
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
        static void fillProductsInWarehouse()
        {
            _warehouse.Add(new ProductInWarhouse()
            {
                Product = _products[0],
                Count = 8
            });
            _warehouse.Add(new ProductInWarhouse()
            {
                Product = _products[0].RelatedProducts[0],
                Count = 28
            });
            _warehouse.Add(new ProductInWarhouse()
            {
                Product = _products[0].RelatedProducts[1],
                Count = 64
            });
            _warehouse.Add(new ProductInWarhouse()
            {
                Product = _products[0].RelatedProducts[2],
                Count = 90
            });
            _warehouse.Add(new ProductInWarhouse()
            {
                Product = _products[1],
                Count = 19
            });
            _warehouse.Add(new ProductInWarhouse()
            {
                Product = _products[1].RelatedProducts[0],
                Count = 4
            });
            _warehouse.Add(new ProductInWarhouse()
            {
                Product = _products[1].RelatedProducts[1],
                Count = 37
            });
            _warehouse.Add(new ProductInWarhouse()
            {
                Product = _products[2],
                Count = 2
            });
            _warehouse.Add(new ProductInWarhouse()
            {
                Product = _products[2].RelatedProducts[0],
                Count = 4
            });
            _warehouse.Add(new ProductInWarhouse()
            {
                Product = _products[2].RelatedProducts[1],
                Count = 5
            });
        }
        static string getShortText(string text, int length)
        {
            if (text.Length <= length)
                return text;
            if (text.Length - length == 1)
                length--;
            return text.Remove(length) + "..";
        }
        static void printDashSetandResetColor(ConsoleColor color)
        {
            Console.ResetColor();
            Console.Write(" | ");
            Console.ForegroundColor = color;
        }
    }
}
