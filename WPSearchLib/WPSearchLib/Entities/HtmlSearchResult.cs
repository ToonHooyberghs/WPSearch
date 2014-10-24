using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WPSearchLib.Entities
{
    public class HtmlSearchResult
    {
        public HtmlNode MasterItem { get; set; }
        public HtmlNode Name { get; set; }
        public HtmlNode Price { get; set; }
        public HtmlNode ShippingCost { get; set; }
        public HtmlNodeCollection Info { get; set; }

        public HtmlSearchResult(HtmlNode masterItem, HtmlNode name, HtmlNode price, HtmlNode shippingCost, HtmlNodeCollection info)
        {
            MasterItem = masterItem;
            Name = name;
            Price = price;
            ShippingCost = shippingCost;
            Info = info;
        }
    }
}
