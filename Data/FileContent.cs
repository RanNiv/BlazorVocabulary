using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace Data
{

    public record TransLate
    {
        public string Word { get; init; }
        public string Translation { get; init; }

        public string Example { get; init; }

    }

    public class FileContent

    {

        FileInfo file;
        public string FileText { get; set; }
        private IHtmlCollection<IElement> TrTags;

        public List<TransLate> TranslateList { get; set; } = new List<TransLate>();
        public FileContent()
        {
            DirectoryInfo dir1 = new DirectoryInfo("StaticFiles");
            file = dir1.GetFiles().FirstOrDefault(x => x.Name == "SAT Vocabulary.txt");
            StreamReader reader = new StreamReader(file.OpenRead());
            this.FileText = reader.ReadToEnd();

            var T = Task.Run(async () => await CreateTagList());

        }


        private async Task CreateTagList()
        {

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(this.FileText));
            var documentElement = document.DocumentElement;
            TrTags = documentElement.QuerySelectorAll("tr");
            var tr = TrTags.FirstOrDefault().GetType();
            TrTags.ToList().ForEach(x => CreateTranslationRecord(x));

        }


        private void CreateTranslationRecord(IElement element)
        {
            var tds = element.Children;
            var translate = new TransLate
            {
                Word = tds[0].TextContent,
                Translation = tds[1].TextContent,
                Example = tds[2].TextContent
            };
            this.TranslateList.Add(translate);

        }



    }
}