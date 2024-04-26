using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using cart_service_tests.Helper;
using cart_service.Model;

namespace cart_service_tests;

public class ShoppingListController
{
    [Theory]
    [InlineData("Einkaufszettel")]
    public async Task PostOne(string name)
    {
        var myHttpClient = MyHttpClient.Create();
        var newList = new ShoppingList();
        newList.Name = name;
        
        var response = await myHttpClient.PostAsJsonAsync("/ShoppingList", newList);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData("")]
    public async Task PostOneNoName(string name)
    {
        var myHttpClient = MyHttpClient.Create();
        var newList = new ShoppingList();
        newList.Name = name;
        
        var response = await myHttpClient.PostAsJsonAsync("/ShoppingList", newList);
        int i = 5;
    }
}