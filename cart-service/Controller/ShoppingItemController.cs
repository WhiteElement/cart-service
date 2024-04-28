using cart_service.Auxillary;
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

    [HttpGet()]
    public async Task<List<ShoppingItem>> GetAll()
    {
        return await _shoppingItemService.GetAll();
    }
    
    [HttpPost("{shoppingListId:int}")]
    public async Task<IActionResult> AddOne(int shoppingListId, [FromBody] ShoppingItem shoppingItem)
    {
        var resultWrapper = await _shoppingItemService.AddItemToList(shoppingListId, shoppingItem);

        if (resultWrapper.HasError)
            return BraunActionResult.Create(resultWrapper);

        return Ok(resultWrapper.Data);
    }
    
    [HttpDelete("{shoppingItemId:int}")]
    public async Task<IActionResult> DeleteOne(int? shoppingItemId)
    {
        var resultWrapper = await _shoppingItemService.DeleteItem(shoppingItemId);

        if (resultWrapper.HasError)
            return BraunActionResult.Create(resultWrapper);

        return Accepted($"deleted ShoppingItem with Id {shoppingItemId}");
    }
    
    [HttpDelete("multiple")]
    public async Task<IActionResult> DeleteMultiple(int[] shoppingItemIds)
    {
        var resultWrapper = await _shoppingItemService.DeleteItems(shoppingItemIds);

        if (resultWrapper.HasError)
            return BraunActionResult.Create(resultWrapper);

        return Ok(resultWrapper.Data);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchOne([FromBody] ShoppingItem shoppingItem)
    {
        var resultWrappper = await _shoppingItemService.RenameItems(shoppingItem);

        if (resultWrappper.HasError)
            return BraunActionResult.Create(resultWrappper);

        return Ok(resultWrappper);
    }
    //TODO Patch -> Item umbenennen
    // [HttpP]
    
    
}

