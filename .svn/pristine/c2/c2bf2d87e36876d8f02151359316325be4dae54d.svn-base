using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using SeleniumFramework.Utilities.XmlUtil;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace SeleniumFramework.Tests
{
    [TestFixture]
    public class StackListener
    {
        protected IWebDriver driver;
        protected string profile;
        protected string environment;

        public StackListener(string profile, string environment)
        {
            this.profile = profile;
            this.environment = environment;
        }



        [SetUp]
        public void Init()
        {
           //XmlReader xmlReader = new XmlReader();
           // xmlReader.LoadXML(GlobalVariables.EnvConfigurationPath);

           // GlobalVariables.BrowserStack_Parallel = xmlReader.ReadTagValue("parallel", "capabilities");

            NameValueCollection caps = ConfigurationManager.GetSection("capabilities/parallel") as NameValueCollection;  //.GetSection("capabilities/" + profile) as NameValueCollection;
            NameValueCollection settings = ConfigurationManager.GetSection("environments/" + environment) as NameValueCollection;

            DesiredCapabilities capability = new DesiredCapabilities();
          
            foreach (string key in caps.AllKeys)
            {
                capability.SetCapability(key, caps[key]);
            }

            foreach (string key in settings.AllKeys)
            {
                capability.SetCapability(key, settings[key]);
            }

            String username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            if (username == null)
            {
                username = ConfigurationManager.AppSettings.Get("user");
            }

            String accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            if (accesskey == null)
            {
                accesskey = ConfigurationManager.AppSettings.Get("key");
            }


            capability.SetCapability("os", "windows");
            capability.SetCapability("os_version", "10");
            capability.SetCapability("browser", "chrome");
            capability.SetCapability("browser_version", "62.0");
            capability.SetCapability("browserstack.local", "false");
            capability.SetCapability("browserstack.console", "errors");

            capability.SetCapability("browserstack.user", "yogeshpawar7");
            capability.SetCapability("browserstack.key", "jvhFGqXMWapAU5hKkxnm");


            String appId = Environment.GetEnvironmentVariable("BROWSERSTACK_APP_ID");
            if (appId != null)
            {
                capability.SetCapability("app", appId);
            }

          
            string server = ConfigurationManager.AppSettings.Get("server");

            driver = new RemoteWebDriver(new Uri("http://" + server + "/wd/hub/"), capability);

        }
    }
}