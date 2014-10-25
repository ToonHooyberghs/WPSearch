using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPSearchLib.Entities.Browsers;
using WPSearchLib.Entities.Browsers.PhantomJS;
using WPSearchLib.Entities.Providers;
using WPSearchLib.Interfaces;

namespace WPSearchConsole
{
    class Program
    {
        public static Collection<WPSearchLib.Interfaces.IProvider> Providers = new Collection<IProvider>()
            {
                new AmazonProvider("AMAZON"),
                new EbayProvider("EBAY"),
                new AmazonProvider("AMAZON")
            };

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            BaseBrowser browser = new PhantomBrowser();

            string searchArg = "KGS216M";
            searchArg = searchArg.Replace(" ", "+");

            decimal minValue = 150;
            decimal maxValue = 350;

            foreach (IProvider provider in Providers)
            {
                var doc = browser.SearchItem(string.Format(provider.GetUrl(), searchArg));

                foreach (ISearchResult searchResult in provider.GetSearchResults(doc, minValue, maxValue))
                {
                    Console.WriteLine(searchResult.Summarize());
                    Console.WriteLine(new string('_', 50));
                }
            }

            stopwatch.Stop();
            Console.WriteLine("Total searching time : " + Math.Round(stopwatch.Elapsed.TotalSeconds,2));

            Console.ReadKey();

        }
    }
}
