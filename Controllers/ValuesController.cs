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
    public string GetFileContent ()
    {
      
      //test
      //test2
    return _data.FileText;

  


    }



    }