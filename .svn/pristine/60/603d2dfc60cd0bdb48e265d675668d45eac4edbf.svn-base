using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.reportUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Pages.Day1
{
    public class UnsupportedBrowserMessage1 : UIActions
    {
        //public static IWebElement Username => driver.FindElement(By.CssSelector("#email"));
        public static IWebElement BrowserNotSupportedMessageFX => driver.FindElement(By.XPath("//h2[text()='Unsupported Browser']"));
        public static IWebElement BrowserNotSupportedMessageIE => driver.FindElement(By.XPath("//h2[text()='This site is not secure']"));
        public static IWebElement Tab_Activities => driver.FindElement(By.CssSelector("#activities-tab"));
        public static IWebElement Tab_Users => driver.FindElement(By.CssSelector("#users-tab"));
        public static IWebElement Tab_Organisations => driver.FindElement(By.CssSelector("#organisation-tab"));
        public static IWebElement Tab_Stream => driver.FindElement(By.CssSelector("#stream-tab"));
        public void Verify9EyeApplicationOnChromeWithAdminUser()
        {
            try
            {
                IsElementDisplayedNEnabled(Tab_Activities);
                ExtentReportUtil.report.Log(Status.Pass, "Activities tab displayed successfully.");
                IsElementDisplayedNEnabled(Tab_Users);
                ExtentReportUtil.report.Log(Status.Pass, "Users tab displayed successfully.");
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Verify9EyeApplicationInChrome() method " + ex.ToString());
                TakeScreenshot();

            }
        }

        public void Verify9EyeApplicationOnChromeWithSuperUser()
        {
            try
            {
                IsElementDisplayedNEnabled(Tab_Organisations);
                ExtentReportUtil.report.Log(Status.Pass, "Organisations tab displayed successfully.");
                IsElementDisplayedNEnabled(Tab_Users);
                ExtentReportUtil.report.Log(Status.Pass, "Users tab displayed successfully.");
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Verify9EyeApplicationInChrome() method " + ex.ToString());
                TakeScreenshot();

            }
        }

        public void Verify9EyeApplicationOnChromeWithOperatorUser()
        {
            try
            {
                IsElementDisplayedNEnabled(Tab_Activities);
                ExtentReportUtil.report.Log(Status.Pass, "Activities tab displayed successfully.");
                IsElementDisplayedNEnabled(Tab_Stream);
                ExtentReportUtil.report.Log(Status.Pass, "Stream tab displayed successfully.");
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Verify9EyeApplicationInChrome() method " + ex.ToString());
                TakeScreenshot();

            }
        }

        public void Verify9EyeApplicationOnFireFox()
        {
            try
            {
                driver = new FirefoxDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://9idevtest01.beatsystems.com/eye/login");
                IsElementDisplayedNEnabled(BrowserNotSupportedMessageFX);
                ExtentReportUtil.report.Log(Status.Fail, "999i Launched successfully.");
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Verify9EyeApplicationOnFireFox() method " + ex.ToString());
                TakeScreenshot();

            }
        }

        public void Verify9EyeApplicationOnIE()
        {
            try
            {
                driver = new InternetExplorerDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://9idevtest01.beatsystems.com/eye/login");
                IsElementDisplayedNEnabled(BrowserNotSupportedMessageIE);
                ExtentReportUtil.report.Log(Status.Fail, "999i Launched successfully.");
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Verify9EyeApplicationOnIE() method " + ex.ToString());
                TakeScreenshot();

            }
        }

    }
}
