using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

namespace WPSearchLib.Entities.Browsers
{
    public class SimpleHtmlBrowser :BaseBrowser 
    {
        HtmlWeb WebAccess { get; set; }

        public SimpleHtmlBrowser()
        {
          WebAccess = new HtmlWeb();
        }

        protected override string Search(string url)
        {
            var pageSource = WebAccess.Load(url);
            return pageSource.ToString();
        }

        protected async override Task<string> SearchAsync(string url)
        {
            var task = Task.Run(() => Search(url));
            return await task; 
        }
        
    }
}
