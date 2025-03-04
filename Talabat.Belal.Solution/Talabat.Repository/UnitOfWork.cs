using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
        // unit of work call DbContext that call Database
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;

        //private Dictionary<string, GenericRepository<BaseEntity>> _repositories; 
        private Hashtable _repositories; 


        public UnitOfWork(StoreContext storeContext) // ask CLR for creating object from DbContext Implecitly
        {
            this._storeContext = storeContext;
            //_repositories = new Dictionary<string, GenericRepository<BaseEntity>>()
            _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            #region using Dictionary
            //var key = typeof(TEntity).Name;
            //if (!_repositories.ContainsKey(key))
            //{
            //    var repository = new GenericRepository<TEntity>(_storeContext) as GenericRepository<BaseEntity>;
            //    _repositories.Add(key, repository);
            //}

            //return _repositories[key] as IGenericRepository<TEntity>;
            #endregion

            #region using HashTable
            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_storeContext) ;
                _repositories.Add(key, repository);
            }

            return _repositories[key] as IGenericRepository<TEntity>; 
            #endregion
        }

        public async Task<int> CompleteAsync()
            => await _storeContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _storeContext.DisposeAsync();



        // automatic property 
        // compiler will generate backing field => hidden parameter attribute 
        // for each property => create get and set
        //public IGenericRepository<Product> ProductsRepo {get; set;}
        //public IGenericRepository<ProductBrand> ProductBrandsRepo {get; set;}
        //public IGenericRepository<ProductCategory> ProductCategorysRepo {get; set;}
        //public IGenericRepository<DeliveryMethod> DeliveryMethodsRepo {get; set;}
        //public IGenericRepository<OrderItem> OrderItemsRepo {get; set;}
        //public IGenericRepository<Order> OrdersRepo {get; set;}

    }
}
