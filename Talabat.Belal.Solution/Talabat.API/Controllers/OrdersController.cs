using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{
  
  [Authorize]
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



        [ProducesResponseType(typeof(OrderToReturnDTO) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status400BadRequest)]
        [HttpPost] // POST: /api/Orders
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email)!;
            var address = _mapper.Map<AddressDTO, Address>(orderDTO.ShippingAddress);

           var order =   await _orderService.CreateOrderAsync(
                buyerEmail,
                orderDTO.BasketId,
                orderDTO.DelivaryMethodId,
                address
                );


            if (order is null)
                return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<Order , OrderToReturnDTO>(order));

        }




        [HttpGet] // GET: api/orders
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrderForUser()
         {

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email)!;
          var orders =  await _orderService.GetOrdersForUserAsync(buyerEmail);
            if (orders is null)
                return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<IReadOnlyList<Order> , IReadOnlyList<OrderToReturnDTO>>(orders));
        }


        [ProducesResponseType(typeof(OrderToReturnDTO) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] // GET: /api/orders/1
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderByIdForUser(int id )
        {

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email)!;

            var order =await _orderService.GetOrderByIdForUserAsync(id  , buyerEmail);

            if (order is null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Order , OrderToReturnDTO>(order));
        }


        [HttpGet("GetDeliveryMethods")]
        public async Task<ActionResult<DeliveryMethod>> GetDeliveryMethods()
        {
            var deliveryMethods =await  _orderService.GetDeliveryMethodsAsync();

            return Ok(deliveryMethods);
        }
    }
}
