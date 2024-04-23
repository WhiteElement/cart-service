using cart_service.Model;
using cart_service.Service;
using Microsoft.AspNetCore.Mvc;

namespace cart_service.Controller;

[ApiController]
[Route("[controller]")]
public class ShoppingListController(ShoppingListService shoppingListService, ShoppingItemService shoppingItemService) : ControllerBase
{
    private ShoppingListService _shoppingListService = shoppingListService;

    [HttpGet]
    public async Task<List<ShoppingList>> GetAll()
    {
        return await _shoppingListService.GetAll();
    }
    
    [HttpGet("/{shoppingListId:int}")]
    public async Task<ShoppingList> GetOne(int shoppingListId)
    {
        return await _shoppingListService.GetList(shoppingListId);
    }

    [HttpPost]
    public async Task<ShoppingList> AddNew(ShoppingList list)
    {
        return await _shoppingListService.AddNew(list);
    }

}