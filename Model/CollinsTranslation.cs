using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AngleSharp.Html.Dom;

namespace Collins
{

    [Flags]
    public enum TranslaeComplementary
    {

        Skip,
        remove,

        addTranslationFromGoogle,
        addTranslationFromMorfix,

        AddTranslationFromCollins,

        CompleteTranslation = addTranslationFromMorfix | AddTranslationFromCollins

    }


    public enum LoadingTechnique
    {
        readeyList,
        combineList

    }


      public enum BookFrequent
    {
        
        Low,
        High

    }


    public interface ITranslation
    {
        string Entry { get; init; }
        string Description { get; set; }

        

        
        

    }


    public record TextEntry : ITranslation
    {
        public string Entry { get; init; }
        public string Description { get; set; }

        public string EnglishDescription { get; init; }

        public BookFrequent Frequent { get; set; }

        public bool IsInBookVocabulary {get;set;}

        static List<string>TextList=new List<string>();


      


    }


    public record MorfixEntry : ITranslation
    {
        public string Entry { get; init; }

        [JsonPropertyName("TranslationFull")]
        public string Description { get; set; }

    }


    public class CollinsEntry : ITranslation
    {
        private Regex reg = new Regex(@"\d.");

        [JsonPropertyName("name")]
        public string Entry { get; init; }
        /*
        [JsonPropertyName("headline")]
        public string Headline { get; init; }
        */

        //public string Description { get; init; }
        private string description;
        [JsonPropertyName("description")]
        public string Description
        {
            get { return description; }
            set { description = setText(value); }
        }


        private string setText(string text)
        {
            StringBuilder sb = new StringBuilder();
            var matches = reg.Split(text);
            foreach (var item in matches)
                sb.AppendLine(item);

            return sb.ToString();


        }

        public CollinsEntry(string entry, string description) => (this.Entry, this.Description) = (entry, description);
    }

    public class EnrtryNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            //if(name=="name")
            return name.ToUpper();
            //   else return name;
        }
    }

    public class CollinsTranslation : VocabularyScript
    {
        protected override string Path { get; } = @"C:\Users\Owner\Desktop\collins.txt";

        public List<CollinsEntry> TagList { get; set; } = new List<CollinsEntry>();

        protected override void GetScriptTag()
        {
            IEnumerable<IHtmlScriptElement> scriptElements = base.document.QuerySelectorAll("script").Select(x => x as IHtmlScriptElement);
            var scriptJsonTag = scriptElements.FirstOrDefault(x => x.Type == "application/ld+json").TextContent;
            var jsonDocument = JsonDocument.Parse(scriptJsonTag);
            //      var jsonObjectList = jsonDocument.RootElement.EnumerateObject();
            var jsonTermArray = jsonDocument.RootElement.EnumerateObject().FirstOrDefault(x => x.Name == "hasDefinedTerm");
            var jsonParsedArray = JsonDocument.Parse(jsonTermArray.Value.ToString());

            var jsonArray = jsonParsedArray.RootElement.EnumerateArray();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new EnrtryNamingPolicy()
                //WriteIndented = true
            };


            foreach (var jsonItem in jsonArray)
            {
                var jsonObject = JsonSerializer.Deserialize<CollinsEntry>(jsonItem.ToString(), options);
                TagList.Add(new CollinsEntry(jsonObject.Entry, jsonObject.Description));
            }
        }

    }


    public record Column
    {

        public int Index { get; init; }
        public string EnglishTerm { get; init; }

        public string HebrewTerm { get; init; }

    }

    public record CollinsDescription
    {

        public string Entry { get; init; }

        public string Description { get; init; }

    }



    public class LoadDescriptions
    {

        public List<CollinsDescription> CollinsList { get; set; }
        private string filePath = @"C:\Users\Owner\Desktop\Collins-list.txt";

        public LoadDescriptions()
        {
            CollinsList = new();


        }

        public IEnumerable<CollinsDescription> GetData()
        {
            FileInfo file = new FileInfo(filePath);
            using StreamReader stream = new StreamReader(file.OpenRead());
            while (stream.Peek() != -1)
            {
                string text = stream.ReadLine();
                string[] split = text.Split('-');
                CollinsList.Add(new CollinsDescription { Entry = split[0], Description = split[1] });
            }

            return CollinsList;

        }


        public IEnumerable<Column> GetColumns()
        {
            int n = 0;
            List<Column> ColumnList = new();
            ColumnList.Add(new Column { Index = n++, EnglishTerm = "Entry", HebrewTerm = "מילה" });
            ColumnList.Add(new Column { Index = n++, EnglishTerm = "Translation", HebrewTerm = "תרגום" });
            return ColumnList;

        }






    }

}