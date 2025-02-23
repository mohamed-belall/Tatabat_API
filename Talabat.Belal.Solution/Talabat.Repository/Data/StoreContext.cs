using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Repository.Data.Config;


// Add-Migration "OrderModules" -Context StoreContext -Output /Data/Migrations


// to use this modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
using System.Reflection;
using Talabat.Core.Entities.Order_Aggregate;

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

            #region old fashion
            //modelBuilder.ApplyConfiguration(new ProductConfigurations());
            //modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            #endregion

            #region new (using assemply)
            // apply configuration from all IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
            #endregion


        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }




        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("");
        //}

    }
}
