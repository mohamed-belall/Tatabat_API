using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{
  
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(
            IOrderService orderService,
            IMapper mapper
            )
        {
            this._orderService = orderService;
            this._mapper = mapper;
        }



        [ProducesResponseType(typeof(Order) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status400BadRequest)]
        [HttpPost] // POST: /api/Orders
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var adress = _mapper.Map<AddressDTO, Address>(orderDTO.ShippingAddress);

           var order =   await _orderService.CreateOrderAsync(
                orderDTO.BuyerEmail,
                orderDTO.BasketId,
                orderDTO.DelivaryMethodId,
                adress
                );


            if (order is null)
                return BadRequest(new ApiResponse(400));

            return Ok(order);

        }

        [HttpGet] // GET: api/orders?email=mohamedbelal.eng@gmail.com
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrderForUser(string email)
        {
          var orders =  await _orderService.GetOrdersForUserAsync(email);
            if (orders is null)
                return BadRequest(new ApiResponse(400));
            return Ok(orders);
        }


        [ProducesResponseType(typeof(Order) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] // GET: /api/orders/1?email=mohamedbelal.eng@gmail.com
        public async Task<ActionResult<Order>> GetOrderByIdForUser(int id , string email)
        {
            var order =await _orderService.GetOrderByIdForUserAsync(id  , email);

            if (order is null) return NotFound(new ApiResponse(404));

            return Ok(order);
        }
    }
}
