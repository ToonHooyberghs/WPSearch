using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WPSearchLib.Entities.Browsers
{
    public abstract  class BaseBrowser
    {
        public abstract string Search(string url);

        public virtual HtmlDocument SearchItem(string url)
        {
            var result = Search(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            return doc;
        }
    }
}
