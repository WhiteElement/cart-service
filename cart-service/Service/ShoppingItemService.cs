using System.Net;
using cart_service.Auxillary;
using cart_service.Model;
using Microsoft.EntityFrameworkCore;

namespace cart_service.Service;

public class ShoppingItemService
{
    public async Task<List<ShoppingItem>> GetAll()
    {
        await using var dbContext = new DbContext.MyDbContext();
        return dbContext.ShoppingItems.ToList();
    }

    public async Task<ShoppingItem> AddRandomItem()
    {
        await using var dbContext = new DbContext.MyDbContext();

        var randomItem = new ShoppingItem()
        {
            Name = Guid.NewGuid().ToString()
        };

        dbContext.ShoppingItems.Add(randomItem);
        await dbContext.SaveChangesAsync();

        return randomItem;
    }

    public async Task<BraunResultWrapper<ShoppingItem>> AddItemToList(int shoppingListId, ShoppingItem shoppingItem)
    {
        var result = new BraunResultWrapper<ShoppingItem>();

        if (string.IsNullOrEmpty(shoppingItem.Name))
            return result.AddErrorAndReturn("No Name for ShoppingItem specified", HttpStatusCode.UnprocessableContent);

        if (shoppingItem.Id != null)
            return result.AddErrorAndReturn("Id is not allowed on ShoppingItem", HttpStatusCode.Forbidden);

        await using var dbContext = new DbContext.MyDbContext();
        var shoppingList = dbContext.ShoppingLists.Include(shoppingList => shoppingList.Items).FirstOrDefault(list => list.Id == shoppingListId);

        if (shoppingList == null)
            return result.AddErrorAndReturn($"There is no ShoppingList with Id:{shoppingListId}", HttpStatusCode.Conflict);
        
        if (shoppingList.Items.Any(item => item.Name == shoppingItem.Name))
            return result.AddErrorAndReturn($"{shoppingItem.Name} is already in ShoppingList", HttpStatusCode.Forbidden);
       
        shoppingList.Items.Add(shoppingItem);
        await dbContext.SaveChangesAsync();

        result.Data = shoppingItem;
        return result;
    }

    public async Task<BraunResultWrapper<ShoppingItem>> DeleteItem(int? shoppingItemId)
    {
        //TODO
        // keine Id angegeben
        // item mit der Id gibt es nicht
        // prüfen ob wirklich gelöscht
        
        var result = new BraunResultWrapper<ShoppingItem>();

        if (shoppingItemId == null) 
            return result.AddErrorAndReturn("No Id for ShoppingItem provided", HttpStatusCode.Forbidden);

        Console.WriteLine("========================================================");
        await using var dbContext = new DbContext.MyDbContext();
        var affectedItem = dbContext.ShoppingItems.Where(item => item.Id == shoppingItemId);
        dbContext.ShoppingItems.RemoveRange(affectedItem);
        
        // if (itemToDelete == null)
            // return result.AddErrorAndReturn($"No ShoppingItem with Id:{shoppingItemId} found", HttpStatusCode.NotFound);

        var rows = await dbContext.SaveChangesAsync();
        return result;
    }
}