using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WPSearchLib.Entities.Browsers
{
    public abstract  class BaseBrowser
    {
        protected abstract string Search(string url);

        protected abstract Task<string> SearchAsync(string url);

        public virtual HtmlDocument SearchItem(string url)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = Search(url);
            var elapsedSearchingTime = stopWatch.Elapsed.TotalSeconds;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("{0} Sec -> Opening Page",Round(elapsedSearchingTime));
            Console.WriteLine("{0} Sec -> Dom Processing", Round(ts.TotalSeconds - elapsedSearchingTime));
            Console.WriteLine("{0} Sec -> Searching : '{1}'", Round(ts.TotalSeconds), url); 
            return doc;
        }

        public async virtual Task<HtmlDocument> SearchItemAsync(string url)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = await SearchAsync(url);
            var elapsedSearchingTime = stopWatch.Elapsed.TotalSeconds;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("{0} Sec -> Opening Page", Round(elapsedSearchingTime));
            Console.WriteLine("{0} Sec -> Dom Processing", Round(ts.TotalSeconds - elapsedSearchingTime));
            Console.WriteLine("{0} Sec -> Searching : '{1}'", Round(ts.TotalSeconds), url);
            return doc;
        }

        private double Round(double number)
        {
            return Math.Round(number, 2);
        }
    }
}
