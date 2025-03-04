using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<DeliveryMethod> _delivaryMethodRepo;
        private readonly IGenericRepository<Order> _ordersRepo;

        public OrderService(
            IBasketRepository basketRepo,
            IGenericRepository<Product> productRepo,
            IGenericRepository<DeliveryMethod> delivaryMethodRepo,
            IGenericRepository<Order> ordersRepo
            )
        {
            this._basketRepo = basketRepo;
            this._productRepo = productRepo;
            this._delivaryMethodRepo = delivaryMethodRepo;
            this._ordersRepo = ordersRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int delivaryMethodId, Address shippingAddress)
        {
            // 1. Get Basket from baskets repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // 2. Get Selected items at basket from basket repo
            var orderItems = new List<OrderItem>();

            if(basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _productRepo.GetAsync(item.Id);

                    var prodcutItemOrder = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);

                    var orderItem = new OrderItem(prodcutItemOrder, product.Price, item.Quantity);

                    orderItems.Add(orderItem);


                }

            }

            // 3. Calculate Subtotal

            var subtotal = orderItems.Sum(orderitem => orderitem.Quantity * orderitem.Price);

            // 4. Get delivaryMethod from delivaryMethod repo
            var delivaryMethod = await _delivaryMethodRepo.GetAsync(delivaryMethodId);


            // 5. create order
            var order = new Order(buyerEmail, shippingAddress, subtotal, delivaryMethodId, delivaryMethod, orderItems);




            // 6. save to database
        }

        public Task<Order> GetOrderByIdForUser(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
