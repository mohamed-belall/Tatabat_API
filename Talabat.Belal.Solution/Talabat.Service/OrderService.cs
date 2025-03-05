using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.Order_Specs;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        ///private readonly IGenericRepository<Product> _productRepo;
        ///private readonly IGenericRepository<DeliveryMethod> _delivaryMethodRepo;
        ///private readonly IGenericRepository<Order> _ordersRepo;

        public OrderService(
            IBasketRepository basketRepo,
            IUnitOfWork unitOfWork
            ///IGenericRepository<Product> productRepo,
            ///IGenericRepository<DeliveryMethod> delivaryMethodRepo,
            ///IGenericRepository<Order> ordersRepo
            )
        {
            this._basketRepo = basketRepo;
            this._unitOfWork = unitOfWork;
           ///this._productRepo = productRepo;
           ///this._delivaryMethodRepo = delivaryMethodRepo;
           ///this._ordersRepo = ordersRepo;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int delivaryMethodId, Address shippingAddress)
        {
            // 1. Get Basket from baskets repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // 2. Get Selected items at basket from basket repo
            var orderItems = new List<OrderItem>();

            if(basket?.Items?.Count > 0)
            {
                var productRepository = _unitOfWork.Repository<Product>();
                foreach (var item in basket.Items)
                {
                    //var product = await _productRepo.GetAsync(item.Id);
                    var product = await productRepository.GetByIdAsync(item.Id);

                    var prodcutItemOrder = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);

                    var orderItem = new OrderItem(prodcutItemOrder, product.Price, item.Quantity);

                    orderItems.Add(orderItem);
                }

            }

            // 3. Calculate Subtotal

            var subtotal = orderItems.Sum(orderitem => orderitem.Quantity * orderitem.Price);

            // 4. Get delivaryMethod from delivaryMethod repo
            //var delivaryMethod = await _delivaryMethodRepo.GetAsync(delivaryMethodId);
            var delivaryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(delivaryMethodId);

            // 5. create order
            var order = new Order(buyerEmail, shippingAddress, subtotal, delivaryMethodId, delivaryMethod, orderItems);

            await _unitOfWork.Repository<Order>().AddAsync(order);


            // 6. save to database
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0)
                return null;

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();

            var spec = new OrderSpecifications(buyerEmail);
            var orders =  await orderRepo.GetAllWithSpecAsync(spec);
            return orders;
        }

        public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();


            var spec = new OrderSpecifications(orderId , buyerEmail);

            var order =  await orderRepo.GetByIdWithSpecAsync(spec);

            return order;
        }

     
    }
}
