using System.Net;
using cart_service.Model;

namespace cart_service.Auxillary;

public class BraunResultWrapper <T>
{
   public T? Data { get; set; } 
   public List<T>? DataList { get; set; }
   public bool HasError { get; set; }
   public string? ErrorMessage { get; set; }
   
   public HttpStatusCode ErrorCode { get; set; }

   public BraunResultWrapper()
   {
      HasError = false;
   }
   public void AddError(string errorMessage, HttpStatusCode statusCode)
   {
      HasError = true;
      ErrorMessage = errorMessage;
      ErrorCode = statusCode;
   }

   public BraunResultWrapper<T> AddErrorAndReturn(string errorMessage, HttpStatusCode statusCode)
   {
      HasError = true;
      ErrorMessage = errorMessage;
      ErrorCode = statusCode;

      return this;
   }
}