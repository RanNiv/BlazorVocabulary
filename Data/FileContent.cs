using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Collins;
using System;


namespace Data
{

    public record TransLate
    {
        public string Word { get; init; }
        public string Translation { get; init; }

        public string[] Examples { get; init; }

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