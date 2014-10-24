using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WPSearchLib.Interfaces;

namespace WPSearchLib.Entities.Providers
{
    public class AmazonProvider : BaseProvider
    {
        public AmazonProvider(string name): base(name)
        {
        }

        #region IProvider Members

        public override string GetUrl()
        {
            return "http://www.amazon.de/s/ref=nb_sb_noss?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&url=search-alias%3Daps&field-keywords={0}";
        }

        #endregion

        #region IProvider Members

        public override IEnumerable<ISearchResult> GetSearchResults(HtmlDocument htmlRoot)
        {
            return GetInternalSearchResults(htmlRoot);
        }

        public override string GetItemPath()
        {
            return "//*[starts-with(@id, 'result_')]";
        }

        public override string GetNamePath()
        {
            return ".//*[contains(@class,'lrg bold')]";
        }
        public override string GetPricePath()
        {
            return ".//*[contains(@class,'newp')]";
        }

        public override string GetInfoPath()
        {
            return ".//*[contains(@class,'price bld')]";
        }

        #endregion
    }
}
