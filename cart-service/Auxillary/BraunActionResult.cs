using Microsoft.AspNetCore.Mvc;

namespace cart_service.Auxillary;

public class BraunActionResult
{
    public static ContentResult Create<T>(BraunResultWrapper<T> resultWrapper)
    {
        return new ContentResult()
           {
               Content = resultWrapper.ErrorMessage, 
               StatusCode = (int)resultWrapper.ErrorCode,
               ContentType = "application/json"
           };
    }
}