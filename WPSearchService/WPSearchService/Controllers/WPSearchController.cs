using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WPSearchLib.Entities.Browsers;
using WPSearchLib.Entities.Browsers.PhantomJS;
using WPSearchLib.Entities.Providers;
using WPSearchLib.Interfaces;

namespace WPSearchService.Controllers
{
    public class WPSearchController : ApiController
    {

        public static Collection<WPSearchLib.Interfaces.IProvider> Providers = new Collection<IProvider>()
            {
                new EbayProvider("Ebay",new PhantomBrowser()),
                new AmazonProvider("Amazon",new PhantomBrowser())
            };

        // GET api/wpsearch/5
        [HttpPost]
        public async Task<IEnumerable<ISearchResult>> Get(string searchWp)
        {
            ICollection<ISearchResult> searchresults = new Collection<ISearchResult>();

            decimal minValue = 200;
            decimal maxValue = 350;

            await Task.WhenAll(Providers.Select(provider =>  AskProvider( searchresults, provider, searchWp, minValue, maxValue)));

            var limitedResults = searchresults.Where(x => x.Price != null && (x.Price >= minValue && x.Price <= maxValue)).OrderBy(x => x.Price).ToList();

            return limitedResults;
        }


        private async static Task<IEnumerable<ISearchResult>> AskProvider(ICollection<ISearchResult> searchResults , IProvider provider, string searchArg, decimal minValue, decimal maxValue)
        {
            var result = await provider.LaunchSearch(searchArg, minValue, maxValue);

            foreach (ISearchResult searchResult in result)
            {
                Console.WriteLine(searchResult.Summarize());
                Console.WriteLine(new string('_', 50));
            }

            AddRange(searchResults, result);

            return result;
        }

        public static void AddRange<T>(ICollection<T> destination, IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                destination.Add(item);
            }
        }
       
    }
}
