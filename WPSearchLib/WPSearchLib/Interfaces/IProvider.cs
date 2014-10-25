using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WPSearchLib.Interfaces
{
    public interface IProvider
    {
        string Name { get; set; }

        string GetUrl();

        Task<IEnumerable<ISearchResult>> LaunchSearch(string url, decimal minRange = Decimal.MinValue,
                                                      decimal maxRange = Decimal.MaxValue);

        ISearchResult CreateEmptySearchResult();

        IEnumerable<ISearchResult> GetSearchResults(HtmlDocument htmlRoot);

        IEnumerable<ISearchResult> GetSearchResults(HtmlDocument htmlRoot, decimal minRange = Decimal.MinValue, decimal maxRange = Decimal.MaxValue);

    }
}
