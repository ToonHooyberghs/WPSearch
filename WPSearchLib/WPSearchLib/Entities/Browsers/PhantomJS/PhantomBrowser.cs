using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.PhantomJS;

namespace WPSearchLib.Entities.Browsers.PhantomJS
{
    public class PhantomBrowser : BaseBrowser , IDisposable
    {
        private PhantomJSDriver phantomJsDriver;

        public PhantomBrowser()
        {
            const string phantomDirectory = @"C:\GitHub\WPSearch\WPSearchLib\packages\PhantomJS.1.9.7\tools\phantomjs\";
            var driverService = PhantomJSDriverService.CreateDefaultService(phantomDirectory);
            driverService.HideCommandPromptWindow = true;
            driverService.LoadImages = false;
            phantomJsDriver = new PhantomJSDriver(driverService);
        }

        protected override string Search(string url)
        {
            phantomJsDriver.Url = url;
            var pageSource = phantomJsDriver.PageSource;
            return pageSource;
        }

        protected async override Task<string> SearchAsync(string url)
        {
            var task = Task.Run(() => Search(url));
            return await task; 
        }


        #region IDisposable Members

        public void Dispose()
        {
           phantomJsDriver.Dispose();
        }

        #endregion
    }
}
