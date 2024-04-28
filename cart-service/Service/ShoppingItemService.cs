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
        var result = new BraunResultWrapper<ShoppingItem>();

        await using var dbContext = new DbContext.MyDbContext();
        var itemToDelete = await dbContext.ShoppingItems.Where(i => i.Id == shoppingItemId).ExecuteDeleteAsync();
        
        if (itemToDelete == 0)
            return result.AddErrorAndReturn($"No ShoppingItem with Id:{shoppingItemId} found", HttpStatusCode.NotFound);

        await dbContext.SaveChangesAsync();
        return result;
    }

    public async Task<BraunResultWrapper<string>> DeleteItems(int[] shoppingItemIds)
    {
        var result = new BraunResultWrapper<string>();

        if (shoppingItemIds.Length == 0)
            return result.AddErrorAndReturn("No ShoppingIds provided for Deletion", HttpStatusCode.Conflict);
        
        await using var dbContext = new DbContext.MyDbContext();

        List<int> itemsDeleted = new ();
        foreach (int id in shoppingItemIds)
        {
            int row = await dbContext.ShoppingItems.Where(item => item.Id == id).ExecuteDeleteAsync();

            if (row == 1)
                itemsDeleted.Add(id);
        }

        result.Data = FormatDeletionResult(shoppingItemIds, itemsDeleted);
        return result;
    }

    private string FormatDeletionResult(int[] shoppingItemIds, List<int> itemsDeleted)
    {
        return $"Deleted {itemsDeleted.Count} -> {string.Join(",", itemsDeleted)}\n" +
               $"Input {shoppingItemIds.Length} -> {string.Join(",", shoppingItemIds)}";
    }

    public async Task<BraunResultWrapper<ShoppingItem>> RenameItem(ShoppingItem shoppingItem)
    {
        var result = new BraunResultWrapper<ShoppingItem>();

        if (shoppingItem.Id == null)
            return result.AddErrorAndReturn("No Id provided for ShoppingItem to change the name of", HttpStatusCode.Conflict);

        if (string.IsNullOrEmpty(shoppingItem.Name))
            return result.AddErrorAndReturn("No Name for Renaming provided", HttpStatusCode.Conflict);
        
        await using var dbContext = new DbContext.MyDbContext();

        var affectedRow = await dbContext.ShoppingItems.Where(i => i.Id == shoppingItem.Id)
            .ExecuteUpdateAsync(k => k.SetProperty(j => j.Name, shoppingItem.Name));

        if (affectedRow == 0)
            return result.AddErrorAndReturn($"No ShoppingItem with Id {shoppingItem.Id} found", HttpStatusCode.NotFound);

        result.Data = shoppingItem;
        return result;
    }
}