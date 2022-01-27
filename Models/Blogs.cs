using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Models
{
    public class Blogs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public string ImageUrl { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

}
}
