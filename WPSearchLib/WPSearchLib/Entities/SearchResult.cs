using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPSearchLib.Interfaces;

namespace WPSearchLib.Entities
{
    public class SearchResult : ISearchResult
    {
        public SearchResult()
        {
            Seller = String.Empty;
            Name = String.Empty;
            Price = null;
            ShippingCost = String.Empty;
            Info = string.Empty;
        }

        #region ISearchResult Members

        public string Seller { get; set; }

        public string Name { get; set; }

        public Decimal? Price { get; set; }

        public string ShippingCost { get; set; }

        public string Info { get; set; }

        public string Summarize()
        {
            return "{ \r\n Seller :\t" + Seller + "\r\n Name :\t\t" + Name.Trim() + "\r\n Price :\t" + Price + "\r\n Shipping :\t" + ShippingCost.Trim() + "\r\n Info :\t\t" + Info + "\r\n}";
        }

        #endregion
    }
}
