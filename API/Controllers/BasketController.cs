using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        public IBasketRepository BasketRepository { get; }
        public BasketController(IBasketRepository basketRepository)
        {
            this.BasketRepository = basketRepository;

        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>>  GetBasketById(string id)
        {
            var basket= await this.BasketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket([FromBody]CustomerBasket basket)
        {
            var updatedBasket= await this.BasketRepository.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
             await this.BasketRepository.DeleteBasketAsync(id);
        }

    }
}