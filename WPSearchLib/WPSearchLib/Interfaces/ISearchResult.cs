using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPSearchLib.Interfaces
{
    public interface ISearchResult
    {
        string Seller { get; set; }
        string Name { get; set; }
        decimal? Price { get; set; }
        string ShippingCost { get; set; }
        string Info { get; set; }

        string Summarize();
    }
}
