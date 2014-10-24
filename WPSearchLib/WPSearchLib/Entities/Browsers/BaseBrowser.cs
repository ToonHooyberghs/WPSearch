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

        public virtual HtmlDocument SearchItem(string url)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = Search(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("{0} Sec -> Searching : '{1}'",ts.TotalSeconds,url); 
            return doc;
        }
    }
}
