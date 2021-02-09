using Collins;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blazorvocabulary.Data;
using Blazorvocabulary.Models;
using Microsoft.Extensions.Configuration;

[Route("api/[controller]/[action]")]
[ApiController]
public class ValuesController : ControllerBase
{



    FileContent _data;
    TranslateDbContext _context;
    public ValuesController(FileContent data,TranslateDbContext context)
    {
        _data = data;
        this._context=context;
     

    }


    [HttpGet]
    public IEnumerable<MorfixEntry> Get()
    {
        //LoadTextFile load=new LoadTextFile();

        MorfixTranslation translation = new MorfixTranslation();
        return translation.EntryList;

    }

    [HttpGet]
    public string GetFile()
    {

        DirectoryInfo dir1 = new DirectoryInfo("StaticFiles");
        var file = dir1.GetFiles().FirstOrDefault(x => x.Name.Contains("SAT"));
        return file.FullName;
    }


    [HttpGet]
    public object GetFileData()
    {

        LoadDescriptions descriptions = new LoadDescriptions();

        return new { Data = descriptions.GetData() };

    }





    [HttpGet]
    public IEnumerable<Column> GetColumns()
    {

        LoadDescriptions descriptions = new LoadDescriptions();

        return descriptions.GetColumns();

    }


      [HttpGet]
    public IEnumerable<TranslateList> GetDbData()
    {

       return _context.TranslateLists;

    }




}