using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genie.Core.Entities;
using Genie.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GenieAPI.Controllers;

public class BasketController : BaseAPIController
{
    private readonly IBasketRepository _basketRepository;

    public BasketController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(string id) {
        var basket = await _basketRepository.GetBasketAsync(id);
        return Ok(basket ?? new CustomerBasket(id));
    }
    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    {
        var updateBasket = await _basketRepository.UpdateBasketAsync(basket);
        return Ok(updateBasket);
    }
    [HttpDelete]
    public async Task DeleteBasketAsync(string id) 
    {
        await _basketRepository.DeleteBasketAsync(id);
    }
}
