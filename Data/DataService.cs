using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazorvocabulary.Data;
using Blazorvocabulary.Models;
using Collins;


public interface IRestDataService
{
    Task<IEnumerable<ITranslation>> GetJson();

}

public class DataService
{

     LoadTranslateCollection _collection;
    TranslateDbContext _context;
    HttpClient Client;
    public DataService(HttpClient client,TranslateDbContext context,LoadTranslateCollection collection)
    {
        Client = client;
        Client.BaseAddress = new Uri("https://localhost:5001/");
       _context=context;
       _collection=collection;

       FillDb();

    }

    public Task<IEnumerable<CollinsEntry>> GetJson()
    {

        return Client.GetFromJsonAsync<IEnumerable<CollinsEntry>>("api/values/GetFileContent");
    }


    public Task<IEnumerable<MorfixEntry>> GetMorfixEntries()
    {
        return Client.GetFromJsonAsync<IEnumerable<MorfixEntry>>("api/values/get");
    }


private void FillDb () 
{
    foreach (var item in _collection.EntryList)
    {

        if(item is TextEntry textentry)
        {
            if(textentry.Description is not null)
            {

                if( _context.TranslateLists.FirstOrDefault(x=>x.Entry==textentry.Entry)==null)
                _context.TranslateLists.Add(new TranslateList {Entry=textentry.Entry,TranslationFull=textentry.Description,EnglishTranslate=textentry.EnglishDescription});
            }
     

        }
    }

    //_context.SaveChanges();

}



}