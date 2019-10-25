﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.reportUtil;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Threading;
using OpenQA.Selenium.Chrome;
using SeleniumFramework.Utilities.XmlUtil;
using OpenQA.Selenium.Support.UI;
using SeleniumFramework.Utilities.common;

namespace SeleniumFramework.Pages
{
    class StreamPage : UIActions
    {

        public IWebDriver remotedriver;
        LoginPage login = new LoginPage();
        public static XmlReader xmlReader;
        public static IWebElement Button_StartVideo => driver.FindElement(By.CssSelector("div#home-buttons > div > button[data-i18n='startStreamButtonText']"));
        public static IWebElement Button_StopVideo => driver.FindElement(By.CssSelector("div.stop-stream > button[data-i18n='stopStreamButtonText']"));

        public static IWebElement Link_StreamTab => driver.FindElement(By.CssSelector("#stream-tab"));
        public static IWebElement Input_EmailBox => driver.FindElement(By.CssSelector("#omni"));
        public static IWebElement Input_Reference => driver.FindElement(By.CssSelector("#reference"));
        public static IWebElement Button_Send => driver.FindElement(By.CssSelector("#send-button"));

        //106077
        public static IWebElement Button_EndSession => driver.FindElement(By.CssSelector("#end-session-button"));

        //106240
        public static IWebElement Header_error => driver.FindElement(By.CssSelector("h2.error-message"));

        public void RemoteWebDriver(string devicemode = null)
        {


            DesiredCapabilities capability = new DesiredCapabilities();
            capability.SetCapability("os_version", BrowserStack_BrowserVersion);
            capability.SetCapability("device", BrowserStack_Device);
            capability.SetCapability("real_mobile", BrowserStack_realmobile);
            capability.SetCapability("browserstack.local", BrowserSatck_Local);
            capability.SetCapability("browserstack.console", "errors");
            capability.SetCapability("browserstack.user", "yogeshpawar7");
            capability.SetCapability("browserstack.key", "jvhFGqXMWapAU5hKkxnm");
            if (String.IsNullOrEmpty(devicemode))
            {
                capability.SetCapability("deviceOrientation", BrowserStack_DeviceOrientation);
            }
            else
            {
                capability.SetCapability("deviceOrientation", "landscape");
            }
            remotedriver = new RemoteWebDriver(new Uri("http://" + BrowserStack_Server + "/wd/hub"), capability);


        }

        public void SetCapabilities()
        {
            xmlReader = new XmlReader();

            xmlReader.LoadXML(GlobalVariables.EnvConfigurationPath);
            GlobalVariables.Environment = xmlReader.ReadTagValue("Environment");
            GlobalVariables.Browser = xmlReader.ReadTagValue("Browser");

            GlobalVariables.BrowserStack_Server = xmlReader.ReadTagValue("Server", "BrowserStackConfiguration");
            GlobalVariables.BrowserStack_User = xmlReader.ReadTagValue("UserKey", "BrowserStackConfiguration");
            GlobalVariables.BrowserStack_Key = xmlReader.ReadTagValue("AccessKey", "BrowserStackConfiguration");
            GlobalVariables.BrowserStack_Browser = xmlReader.ReadTagValue("BrowserName", "BrowserStackConfiguration");
            GlobalVariables.BrowserStack_BrowserVersion = xmlReader.ReadTagValue("BrowserVersion", "BrowserStackConfiguration");
            // GlobalVariables.BrowserStack_OS = xmlReader.ReadTagValue("OSName", "BrowserStackConfiguration");
            GlobalVariables.BrowserStack_OSVersion = xmlReader.ReadTagValue("OSVersion", "BrowserStackConfiguration");
            GlobalVariables.BrowserStack_BrowserVersion = xmlReader.ReadTagValue("BrowserVersion", "BrowserStackConfiguration");
            // GlobalVariables.BrowserSatck_Local = xmlReader.ReadTagValue("OSName", "BrowserStackConfiguration");
            GlobalVariables.BrowserStack_realmobile = xmlReader.ReadTagValue("realmobile", "BrowserStackConfiguration");
            GlobalVariables.BrowserStack_Device = xmlReader.ReadTagValue("Device", "BrowserStackConfiguration");
            GlobalVariables.BrowserStack_DeviceOrientation = xmlReader.ReadTagValue("DeviceOrientation", "BrowserStackConfiguration");



        }

        public void StartAndStopStreamingOnRealDevice(string devicemode = null)
        {

            SetCapabilities();
            RemoteWebDriver(devicemode);
            //Common.ReadImap();
            remotedriver.Navigate().GoToUrl(MailRepository.FirstCaller.ElementAt(0));

            Thread.Sleep(5000);

            if (IsDialogPresent(remotedriver))
            {

                IAlert alert = remotedriver.SwitchTo().Alert();
                alert.Accept();
            }

            Thread.Sleep(5000);

            remotedriver.FindElement(By.CssSelector("div#home-buttons > div > button[data-i18n='startStreamButtonText']")).Click();
            Thread.Sleep(5000);
            // Logger.Message("Clicked on Start Button");
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Thread.Sleep(20000);

            IWebElement stopButton = remotedriver.FindElement(By.CssSelector("div.stop-stream > button[data-i18n='stopStreamButtonText']"));
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(10000);
            stopButton.Click();
            //Logger.Message("Clicked on Stop Button");
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(10000);
        }

        public void VerifyExpiredLink(string mail, string errormessage)
        {
            SetCapabilities();
            RemoteWebDriver();

            remotedriver.Navigate().GoToUrl(mail);

            //read the message and verify
            string UIerror = Header_error.GetAttribute("innerText");
            AssertTrue(UIerror.Equals(errormessage));
        }

        bool IsDialogPresent(IWebDriver remotedriver)
        {
            IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(remotedriver);
            return (alert != null);
        }

        public void SendLink(string emaildId)
        {
            //click on stream tab
            ClickOn(Link_StreamTab);

            // input email ID and reference
            Set_Textbox(Input_EmailBox, emaildId);
            Set_Textbox(Input_Reference, "Automation");

            ClickOn(Button_Send);

            //wait for 5 minutes
            //Thread.Sleep(TimeSpan.FromMinutes(5));


        }

        public void EndStreaming()
        {
            //click on End Session button
            Thread.Sleep(10000);

            //click on End Session button
            ClickOn(Button_EndSession);
        }
    }
}
