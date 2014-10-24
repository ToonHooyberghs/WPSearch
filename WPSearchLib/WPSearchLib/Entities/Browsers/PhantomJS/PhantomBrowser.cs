using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.PhantomJS;

namespace WPSearchLib.Entities.Browsers.PhantomJS
{
    public class PhantomBrowser : BaseBrowser
    {
        public override string Search(string url)
        {
            const string PhantomDirectory = @"..\PhantomJS";
            var driverService = PhantomJSDriverService.CreateDefaultService(PhantomDirectory);
            driverService.HideCommandPromptWindow = false;
            driverService.LoadImages = false;
            string pageSource = null;

            using (var phantomDriver = new PhantomJSDriver(driverService))
            {
                phantomDriver.Url = url;
                pageSource = phantomDriver.PageSource;
            }

            return pageSource;

        }
    }
}
