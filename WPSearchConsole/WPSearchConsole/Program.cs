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
                new AmazonProvider("AMAZON",new MockBrowser()),
                //new EbayProvider("EBAY",new MockBrowser()),
                //new AmazonProvider("AMAZON",new PhantomBrowser())
            };

        static void Main(string[] args)
        {
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

            await Task.WhenAll(Providers.Select(provider => AskProvider( provider, searchArg, minValue, maxValue)));
            
            stopwatch.Stop();
            Console.WriteLine("Total searching time : " + Math.Round(stopwatch.Elapsed.TotalSeconds, 2));

            Console.ReadKey();
        }
        
        private async static Task AskProvider( IProvider provider, string searchArg, decimal minValue, decimal maxValue)
        {
            var result = await provider.LaunchSearch(searchArg, minValue, maxValue);

            foreach (ISearchResult searchResult in result)
            {
                Console.WriteLine(searchResult.Summarize());
                Console.WriteLine(new string('_', 50));
            }
        }
        
    }
}
