using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WPSearchLib.Entities.Browsers;
using WPSearchLib.Interfaces;

namespace WPSearchLib.Entities.Providers
{
    public class AmazonProvider : BaseProvider
    {
        public AmazonProvider(string name , BaseBrowser baseBrowser): base(name ,baseBrowser)
        {
        }

        #region IProvider Members

        public override string GetUrl()
        {
            return "http://www.amazon.de/s/ref=nb_sb_noss?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&url=search-alias%3Daps&field-keywords={0}&rh=i%3Aaps%2Ck%3A{0}%2Cp_76%3A419123031";
        }

        #endregion

        #region IProvider Members

        public override IEnumerable<ISearchResult> GetSearchResults(HtmlDocument htmlRoot , string searchArg)
        {
            return GetInternalSearchResults(htmlRoot,searchArg);
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
