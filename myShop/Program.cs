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
            Bonus = 9,
            Event = 12,
            HighRating = 10
        }
        struct Product
        {
            public int Id;
            public string Name;
            public string Description;
            public int Price;
            public Status Status;
        }
        static void Main(string[] args)
        {

        }
    }
}
