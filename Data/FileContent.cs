using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Collins;
using System;
using System.Text.Json;
using System.Text;

namespace Blazorvocabulary.Data
{

    public record TransLate
    {
        public string Word { get; init; }
        public string Translation { get; init; }

        public string[] Examples { get; init; }

    }




    delegate void GetList();

    public class LoadTranslateCollection
    {
        private string filePath = @"C:\Users\Owner\Desktop\";
        LoadingTechnique technique;
        public List<ITranslation> EntryList { get; set; } = new List<ITranslation>();
        public List<string>BookList=new List<string>();

        GetList getList;

       


        public LoadTranslateCollection(string path, LoadingTechnique technique,IEnumerable<string>list)
        {
            this.filePath = this.filePath += path + ".txt";
            this.technique = technique;
            BookList=new List<string>(list);
            switch (technique)
            {
                case LoadingTechnique.readeyList:
                    getList += GetVocabularyList;
                    break;

            }

            getList += AddMorfixEntries;
            getList.Invoke();
        }




        private void GetVocabularyList()
        {
            FileInfo file = new FileInfo(this.filePath);
            using StreamReader reader = new StreamReader(file.OpenRead());
            while (reader.Peek() != -1)
            {
                TextEntry entry1 = new TextEntry();
                string text = reader.ReadLine();
                string[] textArray = text.Split('-');
                if (text.Contains('-'))
                {

                    if (!EntryList.Exists(x => x.Entry == textArray[0]) && textArray.Length > 2)
                        EntryList.Add(new TextEntry { Entry = textArray[0], Description = textArray[1], EnglishDescription = textArray[2] });
                    if (!EntryList.Exists(x => x.Entry == textArray[0] && textArray.Length > 1))
                        EntryList.Add(new TextEntry { Entry = textArray[0], Description = textArray[1] });
                }
                else if (!EntryList.Exists(x => x.Entry == text))
                {
 
                    EntryList.Add(new TextEntry { Entry = text });
                }




                var entry = EntryList[^1] as TextEntry;


                if (entry.Description == null)
                    entry.Description = entry switch
                    {
                        { Entry: "concise" } => "תמציתי",
                        { Entry: "impede" } => "לעכב",
                        { Entry: "groping" } => "גשש, משש",
                        { Entry: "delve" } => "להתעמק",
                        { Entry: "superseded" } => "מוחלף",
                        { Entry: "withstand" } or { Entry: "withstood" } => "עָמַד ב_, עמד בפני, הִתְמוֹדֵד",
                        { Entry: "jumble" } => "עִרבּוּביָה",
                        _ => null
                    };


                List<string> BookWordList = new List<string>();

                List<string> HighFrequentList = new List<string>();

                BookList.ForEach(x=>HighFrequentList.Add(x));
                
              
          

                if (HighFrequentList.Exists(x => x == entry.Entry))
                    entry.Frequent = BookFrequent.High;

            }
        }


        private void AddMorfixEntries()
        {
            MorfixTranslation morfixTranslation = new MorfixTranslation();

            Func<ITranslation, bool> predicateTranslate = p =>
               {

                   return EntryList.Exists(x => x.Entry == p.Entry && x.Description == null);

               };


            foreach (var item in morfixTranslation.EntryList)
            {
                if (predicateTranslate(item))
                    EntryList.FirstOrDefault(x => x.Entry == item.Entry).Description = item.Description;
                else EntryList.Add(item);

            }

        }




    }








    public class LoadTextFile
    {

        string path = @"C:\Users\Owner\Desktop\oxford list.txt";
        private string textFile;

        private string[] fileSplitArray;

        public List<EnglishTranslation> EnglishTranslationList = new List<EnglishTranslation>();
        public LoadTextFile()
        {
            FileInfo file = new FileInfo(path);
            using StreamReader stream = new StreamReader(file.OpenRead());
            textFile = stream.ReadToEnd();
            fileSplitArray = textFile.Split("-----");
            foreach (var item in fileSplitArray)
            {

                EnglishTranslationList.Add(new EnglishTranslation(item));
            }

        }


    }






    public record TranslationPart
    {
        public int index { get; set; }
        public string Trasnlation { get; set; }
        public string Example { get; set; }
    }

    public enum LineStatus
    {
        start,
        regular,
        example
    }

    public class EnglishTranslation
    {

        public List<TranslationPart> Translations = new List<TranslationPart>();
        public string Word { get; set; }

        private int index = 0;


        public EnglishTranslation(string textPart)
        {
            LineStatus status = LineStatus.start;
            StringReader textReader = new StringReader(textPart);
            while (textReader.Peek() != -1)
            {

                string text = textReader.ReadLine();
                if (status == LineStatus.start)
                {
                    this.Word = text;
                    status = LineStatus.regular;
                    continue;
                }

                switch (text)
                {

                    case var st when (st.StartsWith("example:")):
                        Translations[^1].Example = text;
                        status = LineStatus.example;
                        break;
                    case var st when (!st.StartsWith("example:")):
                        Translations.Add(new TranslationPart { index = this.index++, Trasnlation = text });
                        status = LineStatus.regular;
                        break;

                }
            }
        }

    }



    public class FileContent
    {

        public CollinsTranslation translation;
        public FileContent()
        {
            translation = new CollinsTranslation();
            Task.Run(() => this.SaveEntryRecord<CollinsEntry>()).Wait();


        }


        private async Task SaveEntryRecord<T>()
        {

            string path = @"C:\Users\Owner\Desktop\Collins list.txt";
            StreamWriter writer = new StreamWriter(path);
            writer.AutoFlush = true;
            Type type = typeof(T);

            foreach (var translate in translation.TagList)
                foreach (var prop in type.GetProperties())
                {
                    await writer.WriteLineAsync($"{prop.Name}:  {prop.GetValue(translate)}");

                }
        }






    }
}