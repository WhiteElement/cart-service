using cart_service.Model;
using cart_service.Service;
using Microsoft.AspNetCore.Mvc;

namespace cart_service.Controller;

[ApiController]
[Route("[controller]")]
public class ShoppingItemController : ControllerBase
{
    private ShoppingItemService _shoppingItemService;

    public ShoppingItemController(ShoppingItemService shoppingItemService)
    {
        _shoppingItemService = shoppingItemService;
    }

    [HttpGet("/")]
    public async Task<List<ShoppingItem>> GetAll()
    {
        return await _shoppingItemService.GetAll();
    }
    
    [HttpPost("/{shoppingListId:int}")]
    public async Task<ShoppingItem> AddOne(int shoppingListId, [FromBody] ShoppingItem shoppingItem)
    {
        return await _shoppingItemService.AddItemToList(shoppingListId, shoppingItem);
    }
}
