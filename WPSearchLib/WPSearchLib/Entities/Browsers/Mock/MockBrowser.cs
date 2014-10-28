using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPSearchLib.Entities.Browsers
{
    public class MockBrowser :BaseBrowser
    {
        private string _provider { get; set; }
    
        
    public MockBrowser(string provider)
    {
        _provider = provider;
    }

        protected override string Search(string url)
        {
            var str = Assembly.GetExecutingAssembly().Location;

            return File.ReadAllText(String.Format(@"C:\GitHub\WPSearch\WPSearchLib\WPSearchLib\Entities\Browsers\Mock\{0}_KGS216M.html",_provider));
        }

        protected async override Task<string> SearchAsync(string url)
        {
            var task = Task.Run(() => Search(url));
            return await task;
        }
    }
}
