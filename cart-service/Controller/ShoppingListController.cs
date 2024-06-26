using System.Net;
using cart_service.Auxillary;
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
    
    [HttpGet("{shoppingListId}")]
    public async Task<IActionResult> GetOne(int shoppingListId)
    {
        var resultWrapper = await _shoppingListService.GetList(shoppingListId);

        if (resultWrapper.HasError)
            return BraunActionResult.Create(resultWrapper);

        return Ok(resultWrapper.Data);
    }

    [HttpPost]
    public async Task<IActionResult> AddNew(ShoppingList list)
    {
        var resultWrapper = await _shoppingListService.AddNew(list);

        if (resultWrapper.HasError)
           return BraunActionResult.Create(resultWrapper);

        return Ok(resultWrapper.Data!);
    }

    public async Task<IActionResult> RenameOne(ShoppingList list)
    {
        
        var resultWrapper = await _shoppingListService.RenameOne(list);

        if (resultWrapper.HasError)
           return BraunActionResult.Create(resultWrapper);

        return Ok(resultWrapper.Data!);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOne(int? id)
    {
        var resultWrapper = await _shoppingListService.DeleteItem(id);

        if (resultWrapper.HasError)
            return BraunActionResult.Create(resultWrapper);

        return Ok($"deleted ShoppingList with Id {id}");
    }
}