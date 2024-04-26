namespace cart_service_tests.Helper;

public static class MyHttpClient 
{
   public static HttpClient Create()
   {
       var httpClient = new HttpClient(); 
       httpClient.BaseAddress = new Uri("http://127.0.0.1:5134");
       return httpClient;
   }
}