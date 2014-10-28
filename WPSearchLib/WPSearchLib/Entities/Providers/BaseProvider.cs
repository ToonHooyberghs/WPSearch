using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WPSearchLib.Entities.Browsers;
using WPSearchLib.Interfaces;

namespace WPSearchLib.Entities.Providers
{
    public abstract class BaseProvider : IProvider
    {
        public Collection<ISearchResult> SearchResults { get; private set; }

        public BaseBrowser Browser { get; private set; }

        public BaseProvider(string name , BaseBrowser browser)
        {
            Name = name;
            Browser = browser;
            SearchResults = new Collection<ISearchResult>();
        }

        #region IProvider Members


        public string Name { get; set; }

        public async virtual Task<IEnumerable<ISearchResult>> LaunchSearch(string searchArg ,decimal minRange = Decimal.MinValue, decimal maxRange = Decimal.MaxValue)
        {
            string url = string.Format(GetUrl(), searchArg.Replace(' ','+'));
            var doc = await Browser.SearchItemAsync(url);
            return GetSearchResults(doc, searchArg, minRange, maxRange);
        }

        public abstract string GetUrl();

        public abstract IEnumerable<ISearchResult> GetSearchResults(HtmlAgilityPack.HtmlDocument htmlRootdecimal , string searchArg);

        public virtual IEnumerable<ISearchResult> GetSearchResults(HtmlAgilityPack.HtmlDocument htmlRootdecimal, string searchArg, decimal minRange = Decimal.MinValue, decimal maxRange = Decimal.MaxValue)
        {
            return GetInternalSearchResults(htmlRootdecimal,searchArg , minRange, maxRange);
        }

        public virtual IEnumerable<ISearchResult> GetInternalSearchResults(HtmlAgilityPack.HtmlDocument htmlRoot, string searchArg, decimal minRange = Decimal.MinValue, decimal maxRange = Decimal.MaxValue)
        {
            ResetResultCollection();

            string itemPath = GetItemPath();
            string namePath = GetNamePath();
            string pricePath = GetPricePath();
            string shippingCostPath = GetShippingCostPath();
            string infoPath = GetInfoPath();

            var masterItems = SelectNodes(htmlRoot, itemPath);


            if(masterItems == null)
                return new Collection<ISearchResult>();

            for (int i = 0; i < masterItems.Count; i++)
            {
                HtmlNode masterItem = masterItems[i];
                HtmlNode item = null;
                HtmlNode name = null;
                HtmlNode shippingCost = null;
                HtmlNode price = null;
                HtmlNodeCollection info = null;

                item = masterItem;

                if (namePath != null)
                    name = SelectSingleNode(item, namePath);

                if (pricePath != null)
                {
                    price = SelectSingleNode(item, pricePath);
                }

                if (shippingCostPath != null)
                    shippingCost = SelectSingleNode(item, shippingCostPath);

                if (infoPath != null)
                    info = SelectNodes(item, infoPath);


                HtmlSearchResult htmlSearchResult = new HtmlSearchResult(item, name, price, shippingCost, info);

                SearchResults.Add(BuildSearchResult(FormatName(htmlSearchResult), FormatPrice(htmlSearchResult), FormatShippingCost(htmlSearchResult), FormatInfo(htmlSearchResult)));


            }


            var nameContainsSearchArg = SearchResults.Where(x => x.Name.Replace(" ", "").ToLower().Contains(searchArg.Replace(" ", "").ToLower()));
            var limitedResults = nameContainsSearchArg.Where(x => x.Price != null && (x.Price >= minRange && x.Price <= maxRange)).OrderBy(x => x.Price).ToList();
            return limitedResults.Count > 5 ? limitedResults.Take(5) : limitedResults;

        }



        public IEnumerable<ISearchResult> ReturnEmptyResult()
        {
            return new Collection<ISearchResult>();
        }

        public virtual ISearchResult CreateEmptySearchResult()
        {
            return new SearchResult();
        }

        #endregion

        public ISearchResult BuildSearchResult(string name, decimal? price, string shippingCost, string info)
        {
            ISearchResult result = CreateEmptySearchResult();
            result.Seller = Name;
            result.Name = name != null ? name.Trim() : string.Empty;
            result.Price = price;
            result.ShippingCost = shippingCost != null ? shippingCost.Trim() : string.Empty;
            result.Info = info != null ? info.Trim() : string.Empty;

            return result;
        }

        #region IProvider Members

        private decimal? LookupIntOrDecimal(string value)
        {
            Regex regexDecimal = new Regex(@"[0-9]*[\,,.][0-9]*");
            Regex regexInteger = new Regex(@"\d+");

            string result = regexDecimal.Match(value).Value;

            if (string.IsNullOrEmpty(result))
            {
                result = regexInteger.Match(value).Value;
            }

            if (string.IsNullOrEmpty(result))
                return null;

            return Convert.ToDecimal(result);

        }

        private void ResetResultCollection()
        {
            SearchResults = new Collection<ISearchResult>();
        }

        public HtmlNodeCollection SelectNodes(HtmlDocument document, string xPath)
        {
            return document.DocumentNode.SelectNodes(xPath);
        }

        public HtmlNode SelectSingleNode(HtmlDocument document, string xPath)
        {
            return document.DocumentNode.SelectSingleNode(xPath);
        }

        public HtmlNodeCollection SelectNodes(HtmlNode node, string xPath)
        {
            return node.SelectNodes(xPath);
        }

        public HtmlNode SelectSingleNode(HtmlNode node, string xPath)
        {
            return node.SelectSingleNode(xPath);
        }

        public virtual string GetItemPath()
        {
            return null;
        }

        public virtual string GetNamePath()
        {
            return null;
        }

        public virtual string GetPricePath()
        {
            return null;
        }

        public virtual string GetShippingCostPath()
        {
            return null;
        }

        public virtual string GetInfoPath()
        {
            return null;
        }

        public virtual string FormatName(HtmlSearchResult htmlSearchResult)
        {
            return htmlSearchResult.Name != null ? htmlSearchResult.Name.InnerText : string.Empty;
        }

        public virtual Decimal? FormatPrice(HtmlSearchResult htmlSearchResult)
        {
            return LookupIntOrDecimal(htmlSearchResult.Price != null ? htmlSearchResult.Price.InnerText : String.Empty);
        }

        public virtual string FormatShippingCost(HtmlSearchResult htmlSearchResult)
        {
            return LookupIntOrDecimal(htmlSearchResult.ShippingCost != null ? htmlSearchResult.ShippingCost.InnerText : string.Empty).ToString();
        }

        public virtual string FormatInfo(HtmlSearchResult htmlSearchResult)
        {
            if (htmlSearchResult.Info != null)
            {
                var res = htmlSearchResult.Info.Select(x => x.InnerText).ToList();
                return string.Join("\n\r", res);
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
