using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPSearchLib.Entities.Browsers;
using WPSearchLib.Interfaces;

namespace WPSearchLib.Entities.Providers
{
    public class EbayProvider : BaseProvider
    {
        public EbayProvider(string name, BaseBrowser baseBrowser): base(name, baseBrowser)
        {
        }

        #region IProvider Members

        public override string GetUrl()
        {
            return "http://www.benl.ebay.be/sch/i.html?_from=R40&_sacat=0&_nkw={0}&_pppn=r1&scp=ce0&_rdc=1";
        }

        public override IEnumerable<ISearchResult> GetSearchResults(HtmlAgilityPack.HtmlDocument htmlRoot , string searchArg)
        {
            var searchResults = GetInternalSearchResults(htmlRoot , searchArg).ToList();

            return searchResults.Count > 10 ? searchResults.Take(10) : searchResults;
        }


        public override string GetItemPath()
        {
            return "//*[starts-with(@id, 'item')]";
        }

        public override string GetNamePath()
        {
            return ".//*[contains(@class, 'lvtitle')]";
        }

        public override string GetPricePath()
        {
            return ".//*[contains(@class,'lvprice prc')]";
        }

        public override string GetShippingCostPath()
        {
            return ".//*[contains(@class, 'lvshipping')]";
        }

        #endregion
    }
}
