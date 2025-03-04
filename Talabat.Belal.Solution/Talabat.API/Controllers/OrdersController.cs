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
        public async Task<ActionResult> CreateOrder(OrderDTO orderDTO)
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
    }
}
