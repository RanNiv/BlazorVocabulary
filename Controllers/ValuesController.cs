using Collins;
using Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[Route("api/[controller]/[action]")]
[ApiController]
public class ValuesController : ControllerBase
{


FileContent _data;
public ValuesController(FileContent data)
{
    _data=data;
}


    [HttpGet]
    public IEnumerable<string> Get ()
    {
     LoadTextFile load=new LoadTextFile();
        return new string[] {"Test1","Test2"};

    }

  [HttpGet]
    public string GetFile ()
    {
      
      DirectoryInfo dir1=new DirectoryInfo("StaticFiles");
      var file=dir1.GetFiles().FirstOrDefault(x=>x.Name.Contains("SAT"));
        return file.FullName;
    }


    [HttpGet]
    public object GetFileData ()
    {
  
LoadDescriptions descriptions=new LoadDescriptions();

    return  new {Data= descriptions.GetData()};

  


    }


        [HttpGet]
    public IEnumerable<Column> GetColumns ()
    {
  
LoadDescriptions descriptions=new LoadDescriptions();

    return  descriptions.GetColumns();

  


    }




    }