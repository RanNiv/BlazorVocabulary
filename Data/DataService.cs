using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Collins;


public interface IRestDataService 
{
Task<IEnumerable<ITranslation>> GetJson ();

}

public class DataService 
{

HttpClient Client;
public DataService(HttpClient client)
{
     Client=client;
    Client.BaseAddress=new Uri("https://localhost:5001/");
   
   
}

 public Task<IEnumerable<CollinsEntry>> GetJson () 
{


    /*         var response = await client.GetAsync("https://localhost:5001/api/values/GetFileContent");
            return await response.Content.ReadAsStringAsync(); */


return Client.GetFromJsonAsync<IEnumerable<CollinsEntry>>("api/values/GetFileContent");

} 




}