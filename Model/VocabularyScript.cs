using System.IO;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

public abstract class VocabularyScript
    {
        protected abstract string Path { get; set; }
       
        protected IDocument document;
        
        private async Task<string> getFileText()
        {
            FileInfo file = new FileInfo(Path);
            StreamReader str = new StreamReader(file.OpenRead());
            return await str.ReadToEndAsync();

        }

        private async Task<IDocument> GetHtmlDocumentAsync(string source)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(source));
            return document;
        }


        private async Task<IDocument> getDocumentData(string source) => await this.GetHtmlDocumentAsync(source);

        public VocabularyScript()
        {
            var t = Task.Run(getFileText).ContinueWith(x => this.document = getDocumentData(x.Result).Result);
            t.Wait();
            GetScriptTag();
        }


        protected abstract void GetScriptTag();
    }