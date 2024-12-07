using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // we only use this when we inherit from identityDbContext
            // because it has 7 domain entity and it wat to fluent api
            // but in DbContext we don't have 
            //base.OnModelCreating(modelBuilder);



        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("");
        //}

    }
}
