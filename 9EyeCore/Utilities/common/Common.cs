using OpenQA.Selenium.Chrome;
using SeleniumFramework.Utilities.reportUtil;
using System;
using AventStack.ExtentReports;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using SeleniumFramework.UIOperations;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace SeleniumFramework.Utilities.common
{
    
    class Common:GlobalVariables
    {

        public Common LaunchBrowser(string browserType)
        {
            try
            {
                switch (browserType.ToUpper())
                {
                    case "CHROME":
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("disable-infobars");
                        driver = new ChromeDriver(GlobalVariables.projectPath, options);
                        driver.Manage().Window.Maximize();
                        break;
                    case "FIREFOX":
                        //driver = new FirefoxDriver();
                        //driver.Manage().Window.Maximize();
                        //Gecko ghjjjjg
                        //Test comment
                        break;
                    case "IE":
                        //driver = new ChromeDriver();
                        //driver.Manage().Window.Maximize();
                        break;
                    case "EDGE":
                        //driver = new ChromeDriver();
                        //driver.Manage().Window.Maximize();
                        break;
                    case "SAFARI":
                         //driver = new PhantomJSDriver();
                        break;
                    case "CHROMEHEADLESS":
                        ChromeOptions optionsh = new ChromeOptions();
                        optionsh.AddArgument("--headless");
                        optionsh.AddArgument("disable-infobars");
                        driver = new ChromeDriver(optionsh);
                        driver.Manage().Window.Maximize();
                        break;
                    default:
                        break;
                }

                ExtentReportUtil.Test.Log(Status.Pass, "Lauched <b>" + browserType + "</b> browser successfully.");
            }
            catch (Exception e)
            {
                ExtentReportUtil.Test.Log(Status.Fail, "Exception occured in LaunchBrowser => " + e.ToString());               
            }
           
            return new Common();
        }
        
        public void NavigateTo(string URL)
        {
            try
            {
                driver.Navigate().GoToUrl(URL);
                new UIActions().WaitForObject(10); 
                ExtentReportUtil.Test.Log(Status.Pass, "Navigated to => <b>"+URL+"</b>");
            }
            catch (Exception e)
            {
                ExtentReportUtil.Test.Log(Status.Fail, "Exception occured on NavigateTo method- " + e.ToString());              
            }
        }

        public static string GetElementAttributes(IWebElement element)
        {
            GlobalVariables.ElementOperated = "";
            IJavaScriptExecutor JScriptDriver = (IJavaScriptExecutor)driver;
            Dictionary<string, object> attributes = JScriptDriver.ExecuteScript("var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;", element) as Dictionary<string, object>;
            GetElementTitle(attributes);
            GlobalVariables.objectAttributes = "";
            foreach (var item in attributes)
            {

                if (!item.Value.Equals(""))
                {
                    if (!GlobalVariables.objectAttributes.Equals(""))
                    {
                        GlobalVariables.objectAttributes = GlobalVariables.objectAttributes + "\r" + item.Key + " : " + item.Value;
                    }
                    else
                    {
                        GlobalVariables.objectAttributes = item.Key + " : " + item.Value;
                    }
                }
            }
            //Added few formatting for the Extent Reporting.
            GlobalVariables.ElementOperated = "<lable class ='dropdown-toggle' data-toggle='dropdown' data-placement='top' title='" + GlobalVariables.objectAttributes + "'><u><b>" + GlobalVariables.ElementTitle.ToString() + "</b></u></lable>";
            return GlobalVariables.ElementOperated;
        }
        public static string GetElementTitle(Dictionary<string, object> attributes)
        {
            try
            {
                if (attributes.ContainsKey("id"))
                {
                    attributes.TryGetValue("id", out GlobalVariables.ElementTitle);

                }
                else if (attributes.ContainsKey("name"))
                {
                    attributes.TryGetValue("name", out GlobalVariables.ElementTitle);

                }
                else if (attributes.ContainsKey("title"))
                {
                    attributes.TryGetValue("title", out GlobalVariables.ElementTitle);
                }
                else if (attributes.ContainsKey("class"))
                {
                    attributes.TryGetValue("class", out GlobalVariables.ElementTitle);
                }
                else
                {
                    GlobalVariables.ElementTitle = "Element";
                }

                return GlobalVariables.ElementTitle.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetTimeStamp()
        {
            DateTime dt = DateTime.Now;
            return dt.ToString("yyyy_MM_dd_hhmmss");
        }

        public void CloseProcess(string ProcessName)
        {
            try
            {
                foreach (var process in Process.GetProcessesByName(ProcessName))
                {
                    process.Kill();
                }
            }
            catch (Exception)
            {

            }
        }

        public void RefreshPage() {
            driver.Navigate().Refresh();
        }
    }
}
