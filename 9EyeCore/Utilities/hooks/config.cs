﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using SeleniumFramework.Utilities.XmlUtil;
using SeleniumFramework.Utilities.common;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Threading.Tasks;
using SeleniumFramework.Utilities.reportUtil;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using Common = SeleniumFramework.Utilities.common.Common;
using SeleniumFramework.Utilities.hooks;
using SeleniumFramework.BusinessFunctions;

namespace SeleniumFramework.Tests
{
    [TestFixture]
    public class config 
    {
        protected IWebDriver driver;
        protected string profile;
        protected string environment;

        public config(string profile, string environment)
        {
            this.profile = profile;
            this.environment = environment;
        }



        //public static XMLTestDataReader testData;
        [SetUp]
        public void Init()
        {
            XMLTestDataReader testData;
           // xmlReader.LoadXML(GlobalVariables.EnvConfigurationPath);

           // GlobalVariables.BrowserStack_Parallel = xmlReader.ReadTagValue("parallel", "capabilities");

           // NameValueCollection caps = ConfigurationManager.GetSection("capabilities/parallel") as NameValueCollection;  //.GetSection("capabilities/" + profile) as NameValueCollection;
            //NameValueCollection settings = ConfigurationManager.GetSection("environments/" + environment) as NameValueCollection;

            DesiredCapabilities capability = new DesiredCapabilities();
          
            //foreach (string key in caps.AllKeys)
            //{
            //    capability.SetCapability(key, caps[key]);
            //}

            //foreach (string key in settings.AllKeys)
            //{
            //    capability.SetCapability(key, settings[key]);
            //}

            string username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            if (username == null)
            {
                username = ConfigurationManager.AppSettings.Get("user");
            }

            String accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            if (accesskey == null)
            {
                accesskey = ConfigurationManager.AppSettings.Get("key");
            }

            capability.SetCapability("browserstack.debug", "true");
            capability.SetCapability("os", "windows");
            capability.SetCapability("os_version", "10");
            capability.SetCapability("browser", "chrome");
            capability.SetCapability("browser_version", "62.0");
            capability.SetCapability("browserstack.local", "false");
            capability.SetCapability("browserstack.console", "errors");
          

            capability.SetCapability("browserstack.user", "yogeshpawar7");
            capability.SetCapability("browserstack.key", "jvhFGqXMWapAU5hKkxnm");

            string className;
            className = TestContext.CurrentContext.Test.ClassName;
            className = className.Substring(className.LastIndexOf(".") + 1);
            string TestFullName = TestContext.CurrentContext.Test.FullName;
            string TestName = TestFullName.Substring(TestFullName.IndexOf(".", TestFullName.IndexOf(".") + 1) + 1);
            string TestDescription;
            try
            {
                TestDescription = TestContext.CurrentContext.Test.Properties.Get("Description").ToString();
                TestDescription = "<br><font size='1'>" + TestDescription + "</font>";
            }
            catch (System.Exception)
            {
                TestDescription = "";
            }
            ExtentReportUtil.Test = ExtentReportUtil.extent.CreateTest(TestName + " " + TestDescription);
            if (TestDescription.Contains("API"))
            {

            }
            else
            {
                new Common().LaunchBrowser(GlobalVariables.Browser)
                   .NavigateTo(GlobalVariables.URL);
            }
            testData = new XMLTestDataReader();
            //testData.SetXMLName(className);
            GlobalVariables.testCaseID = TestContext.CurrentContext.Test.MethodName;
            String appId = Environment.GetEnvironmentVariable("BROWSERSTACK_APP_ID");
            if (appId != null)
            {
                capability.SetCapability("app", appId);
            }

          
            string server = ConfigurationManager.AppSettings.Get("server");

            //driver = new RemoteWebDriver(new Uri("http://" + server + "/wd/hub/"), capability);

        }

        [TearDown]
        public void EndTest1()
        {
            if (driver != null)
            {
                GlobalVariables.driver.Quit();
            }
        }
    }
}