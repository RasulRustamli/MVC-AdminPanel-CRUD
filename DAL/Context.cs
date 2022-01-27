using FrontToBack.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.DAL
{
    public class Context : IdentityDbContext<AppUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderDesc> SliderDescs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<BlogsSlider> BlogsSliders { get; set; }
        public DbSet<InstagramSlider> InstagramSliders { get; set; }
        public DbSet<Bio> Bios { get; set; }

    }
}