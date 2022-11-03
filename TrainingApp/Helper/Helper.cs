using System;
namespace TrainingApp.Helper
{
    public class TrainingAPI
    {
       public HttpClient Init()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7243/");
            return client;
        }
    }
}

