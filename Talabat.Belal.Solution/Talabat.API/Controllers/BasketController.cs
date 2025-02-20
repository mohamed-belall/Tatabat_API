using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos.Redis;
using Talabat.API.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.API.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,
            IMapper mapper
            )
        {
            this._basketRepository = basketRepository;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
          var basket =   await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }


        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateAsync(CustomerBasketDTO customerBasket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(customerBasket);

            var createOrUpdateBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (createOrUpdateBasket is null)
                return BadRequest(new ApiResponse(400));

            return Ok(createOrUpdateBasket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
