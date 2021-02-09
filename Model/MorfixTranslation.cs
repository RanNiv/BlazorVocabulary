using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Collins;


public class MorfixTranslation : VocabularyScript
{

    public List<MorfixEntry> EntryList = new();
    public string Text { get; set; }
    protected override string Path { get => @"C:\Users\Owner\Desktop\morfix list.txt"; }

    protected override void GetScriptTag()
    {
        var scriptTags = base.document.GetElementsByTagName("script").FirstOrDefault(x => x.TextContent.Contains("InitTotalWordList"));
        this.Text = scriptTags.TextContent;
        Regex regex = new Regex("({.*?})");
        IEnumerable<string> collection = regex.Matches(this.Text).Select(x => x.Value);
        foreach (var item in collection.Where(x => x.Contains("EntryPOCO")))
        {
            var jsonText = item.Replace("{\"EntryPOCO\":", "");
            var morfixEntry = JsonSerializer.Deserialize<MorfixEntry>(jsonText);
            EntryList.Add(morfixEntry);
        }
    }
}



