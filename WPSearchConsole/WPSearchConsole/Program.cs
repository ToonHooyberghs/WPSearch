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
            //Task task = new Task(StartSeachingAsync);
            //task.Start();
            //task.Wait();
            StartSeachingAsync();
            Console.ReadKey();
        }

        private  async static void  StartSeachingAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            string searchArg = "KGS216M";
            searchArg = searchArg.Replace(" ", "+");

            decimal minValue = 150;
            decimal maxValue = 350;

            await Task.WhenAll(Providers.Select(provider => AskProvider(new PhantomBrowser(), provider, searchArg, minValue, maxValue)));
            
            stopwatch.Stop();
            Console.WriteLine("Total searching time : " + Math.Round(stopwatch.Elapsed.TotalSeconds, 2));

            Console.ReadKey();
        }


        private async static Task AskProvider(BaseBrowser browser, IProvider provider , string url, decimal minValue, decimal maxValue)
        {
            var doc = await browser.SearchItemAsync(string.Format(provider.GetUrl(), url));

            foreach (ISearchResult searchResult in  provider.GetSearchResults(doc, minValue, maxValue))
            {
                Console.WriteLine(searchResult.Summarize());
                Console.WriteLine(new string('_', 50));
            }
        }

        private static void StartSearching()
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
            Console.WriteLine("Total searching time : " + Math.Round(stopwatch.Elapsed.TotalSeconds, 2));

            Console.ReadKey();
        }
    }
}
