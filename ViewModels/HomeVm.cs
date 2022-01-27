using FrontToBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.ViewModels
{
    public class HomeVm
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderDesc SliderDesc { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public About About { get; set; }
        public IEnumerable<Expert> Experts { get; set; }
        public IEnumerable<Blogs> Blogs { get; set; }
        public IEnumerable<BlogsSlider> BlogsSliders { get; set; }
        public IEnumerable<InstagramSlider> InstagramSliders { get; set; }




    }
}
