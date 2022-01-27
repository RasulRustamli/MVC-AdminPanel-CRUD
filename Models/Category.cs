using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="bosh qoyma")]
        public string Name { get; set; }
        [MinLength(10,ErrorMessage ="100DEN AZ OLMALIDI")]
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
