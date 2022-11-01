using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        public IBasketRepository BasketRepository { get; }
        private readonly IMapper _mapper;
        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
             _mapper = mapper;
            this.BasketRepository = basketRepository;

        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>>  GetBasketById(string id)
        {
            var basket= await this.BasketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket([FromBody]CustomerBasketDto basket)
        {
            var CustomerBasket=_mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
            var updatedBasket= await this.BasketRepository.UpdateBasketAsync(CustomerBasket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
             await this.BasketRepository.DeleteBasketAsync(id);
        }

    }
}