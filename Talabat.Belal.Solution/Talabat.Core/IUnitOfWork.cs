﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {



        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;


        Task<int> CompleteAsync();




        // we can have more than one unit of work
        //      if we have more than one repository layer
        //          if we have more than one DbContext







        // create property signature for every and each repository
        //public IGenericRepository<Product> ProductsRepo { get; set; }
        //public IGenericRepository<ProductBrand> ProductBrandsRepo { get; set; }
        //public IGenericRepository<ProductCategory> ProductCategorysRepo { get; set; }
        //public IGenericRepository<DeliveryMethod> DeliveryMethodsRepo { get; set; }
        //public IGenericRepository<OrderItem> OrderItemsRepo { get; set; }
        //public IGenericRepository<Order> OrdersRepo { get; set; }

    }
}
