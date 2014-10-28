using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPSearchLib.Entities.Browsers
{
    public class MockBrowser :BaseBrowser
    {
        protected override string Search(string url)
        {
            return File.ReadAllText(@"C:\GitHub\WPSearch\WPSearchLib\WPSearchLib\Entities\Browsers\Mock\KGS216M.html");
        }

        protected async override Task<string> SearchAsync(string url)
        {
            var task = Task.Run(() => File.ReadAllText(@"C:\GitHub\WPSearch\WPSearchLib\WPSearchLib\Entities\Browsers\Mock\KGS216M.html"));
            return await task;
        }
    }
}
