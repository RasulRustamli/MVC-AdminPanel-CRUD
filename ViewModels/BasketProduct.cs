using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.ViewModels
{
    public class BasketProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }
    }
}
