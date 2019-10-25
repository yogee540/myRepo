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
using SeleniumFramework.Pages.Day2;
using System.Collections.Specialized;
using System.Configuration;

namespace SeleniumFramework.Pages.Day0
{
    class VideoStreaming:UIActions
    {
        public IWebDriver remotedriver;
        LoginPage login = new LoginPage();
        LoggingActivity logging = new LoggingActivity();
        Common commoncls = new Common();
        public static XmlReader xmlReader;
        public static IWebElement Button_StartVideo => driver.FindElement(By.CssSelector("div#home-buttons > div > button[data-i18n='startStreamButtonText']"));
        public static IWebElement Button_StopVideo => driver.FindElement(By.CssSelector("div.stop-stream > button[data-i18n='stopStreamButtonText']"));
        
        public static IWebElement Link_StreamTab => driver.FindElement(By.CssSelector("#stream-tab"));
        public static IWebElement Input_EmailBox => driver.FindElement(By.CssSelector("#omni"));
        public static IWebElement Input_Reference => driver.FindElement(By.CssSelector("#reference"));
        public static IWebElement Button_Send => driver.FindElement(By.CssSelector("#send-button"));

        //107376
        public static IWebElement Li_CommunicationTab => driver.FindElement(By.CssSelector("li#tab-comms"));
        public static IWebElement Link_EditButton => driver.FindElement(By.XPath("//a[text()='Edit']"));
        public static IWebElement Label_EmailSubject => driver.FindElement(By.CssSelector("label[for='communication_emailSubject']")); //"input#email-subject"
        public static IWebElement Input_EmailSubject => driver.FindElement(By.CssSelector("input#email-subject"));//communication_receiptEmailSubject receipt-email-subject
        public static IWebElement Label_ReceiptEmailSubject => driver.FindElement(By.CssSelector("label[for='communication_receiptEmailSubject']"));
        public static IWebElement Input_ReceiptEmailSubject => driver.FindElement(By.CssSelector("input#receipt-email-subject"));
        //106077
        public static IWebElement Button_EndSession => driver.FindElement(By.XPath("//div[text()='End Session']"));

        //106240
        public static IWebElement Header_error => driver.FindElement(By.CssSelector("h2.error-message"));
        public static IWebElement Li_30days => driver.FindElement(By.CssSelector(""));
        //TC_37
        public static IWebElement I_VideoLoader => driver.FindElement(By.CssSelector("#remoteVideo p i"));
        public static IWebElement P_AwaitingLiveStream => driver.FindElement(By.XPath("//div[@id='remoteVideo']/div/p[text()='Awaiting Live Stream']"));

        //TC_38
        public static IWebElement P_InvalidMobileMsg => driver.FindElement(By.XPath("//div[@class='input-group top-padding']/following-sibling::p"));

        //TC_39
        public static IWebElement Td_ReferenceLabel => driver.FindElement(By.XPath("//td[text()='Reference: ']"));
        public static IWebElement Td_ReferenceNumber => driver.FindElement(By.XPath("//td[text()='Reference: ']/following-sibling::td"));

        //120959
        public static IWebElement Div_CalleInfoContainer => driver.FindElement(By.CssSelector("div.caller-info-container"));
        public static List<IWebElement> Tr_CalleInfoFields => driver.FindElements(By.CssSelector("table#caller-info-table tr")).ToList();
        public static IWebElement Div_SensitivityFlag => driver.FindElement(By.CssSelector("div#visual-sensitive"));
        public static IWebElement Span_ViewSensitiveSession => driver.FindElement(By.XPath("//span[@class='sensitive-warning']/preceding-sibling::span"));
        public static IWebElement Td_SensitiveSessionLog => driver.FindElement(By.XPath("//td[contains(text(),'sensitive')]"));

        public static IWebElement Button_AddStream => driver.FindElement(By.CssSelector("#addStream"));
        public static IWebElement Input_AddstreamMail => driver.FindElement(By.CssSelector("div.add-stream-content div input"));
        public static IWebElement Button_SendAddStream => driver.FindElement(By.CssSelector("button[title='Send']"));


        //KMS 
        public static IWebElement Login_Header => driver.FindElement(By.CssSelector("h3.panel-title"));
        public static IWebElement AwaitingStreamP => driver.FindElement(By.XPath("//p[text()='Awaiting Live Stream']"));

        List<string> temp_List;

        public void RemoteWebDriver(string devicemode=null)
        {

            DesiredCapabilities capability = new DesiredCapabilities();
                       
            capability.SetCapability("os_version", BrowserStack_OSVersion);
            capability.SetCapability("device", BrowserStack_Device);
            capability.SetCapability("real_mobile", BrowserStack_realmobile);
            capability.SetCapability("browserstack.local", BrowserSatck_Local);
            capability.SetCapability("browserstack.console", "verbose");
            capability.SetCapability("browserstack.networkLogs", "true");
            //browserstack.networkLogs
            capability.SetCapability("browserstack.user", BrowserStack_User);
            capability.SetCapability("browserstack.key", BrowserStack_Key);
            capability.SetCapability("browserstack.debug", "true");

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
            GlobalVariables.BrowserStack_Parallel = xmlReader.ReadTagValue("browserstack.debug", "BrowserStackConfiguration");
        }

        //will need to break this method into two- start first caller stream-> start second caller stream-> stop first caller stream -> stop second caller stream
        // we can also set different capabilities for second caller
        //AddStream("automationresult58@gmail.com");
        //MailRepository.ReadEmail("automationresult58@gmail.com", "Password@123");

        public void StartAndStopStreamingOnRealDevice1(string devicemode=null,bool heavymedia=false)
        {

            SetCapabilities();
            RemoteWebDriver(devicemode);
            MailRepository.ReadEmail("testuserbs007@gmail.com","Password@123");
            remotedriver.Navigate().GoToUrl(MailRepository.FirstCaller.ElementAt(0));
           
            Thread.Sleep(7000);

            if (IsDialogPresent(remotedriver))
            {

                IAlert alert = remotedriver.SwitchTo().Alert();
                alert.Accept();
            }

            Thread.Sleep(3000);
            remotedriver.FindElement(By.CssSelector("div#home-buttons > div > button[data-i18n='startStreamButtonText']")).Click();
            Thread.Sleep(7000);
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }
            IWebElement stopButton;
            Thread.Sleep(5000);
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }
            //if (heavymedia)
            //{
            //    //for (int i = 0; i < 1; i++)
            //   // {
            //        string text = remotedriver.FindElement(By.CssSelector("div.stop-stream > button[data-i18n='stopStreamButtonText']")).Text;
            //        remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            //        Thread.Sleep(TimeSpan.FromSeconds(15));
            //   // }
                
            //}
            //else
            //{
            //    remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            //    Thread.Sleep(7000);
            //}
            //VerifyStreamingStatus();
            stopButton = remotedriver.FindElement(By.CssSelector("div.stop-stream > button[data-i18n='stopStreamButtonText']"));
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(7000);
            stopButton.Click();
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Thread.Sleep(5000);
            remotedriver.Quit();
        }

        public void StartAndStopStreamingOnRealDevice2(string devicemode = null)
        {

            SetCapabilities();
            RemoteWebDriver(devicemode);
            MailRepository.ReadEmail("testuserbs007@gmail.com", "Password@123");
            remotedriver.Navigate().GoToUrl(MailRepository.FirstCaller.ElementAt(0));

            Thread.Sleep(10000);

            if (IsDialogPresent(remotedriver))
            {

                IAlert alert = remotedriver.SwitchTo().Alert();
                alert.Accept();
            }

            Thread.Sleep(3000);

            remotedriver.FindElement(By.CssSelector("div#home-buttons > div > button[data-i18n='startStreamButtonText']")).Click();
            Thread.Sleep(10000);
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }

            Thread.Sleep(5000);
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Thread.Sleep(7000);
            VerifyStreamingStatus();
            IWebElement stopButton = remotedriver.FindElement(By.CssSelector("div.stop-stream > button[data-i18n='stopStreamButtonText']"));
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(10000);
            stopButton.Click();
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Thread.Sleep(5000);
            remotedriver.Quit();
        }

        public void GracePeriodTakePhoto(string devicemode = null)
        {

            SetCapabilities();
            MailRepository.ReadEmail("testuserbs007@gmail.com", "Password@123");
            RemoteWebDriver(devicemode);
            remotedriver.Navigate().GoToUrl(MailRepository.FirstCaller.ElementAt(0));

            Thread.Sleep(10000);

            if (IsDialogPresent(remotedriver))
            {

                IAlert alert = remotedriver.SwitchTo().Alert();
                alert.Accept();
            }

            Thread.Sleep(7000);

            IWebElement takephoto= remotedriver.FindElement(By.CssSelector("div#home-buttons > div > button[data-i18n='takePhotoButtonText']"));
            Thread.Sleep(2000);
            takephoto.Click();
            // Logger.Message("Clicked on Start Button");
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }

            Thread.Sleep(5000);
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Thread.Sleep(7000);

                       remotedriver.Quit();
        }

        public void StartAndStopStreamingOnRealDeviceExpiredLink(string devicemode = null)
        {

            SetCapabilities();
            RemoteWebDriver(devicemode);
            remotedriver.Navigate().GoToUrl("https://qatesting.eye999.co.uk/eye/c.html?id=fmNfMWEzMDE4ODMtOTM4NS00M2QzLWFiOWUtNjFiNDQ4YzhmMmNiwrEyYTMwMTg2My1hYmI3LTRlNzktYTJjNi01MWMxZTJmNGZjY2Q%3D");


            Thread.Sleep(5000);

            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Thread.Sleep(7000);

           
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(10000);
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Thread.Sleep(5000);
            remotedriver.Quit();
        }

        public void VerifyExpiredLink(string mail,string errormessage)
        {
            SetCapabilities();
            RemoteWebDriver();

            remotedriver.Navigate().GoToUrl(mail);

            //read the message and verify
            string UIerror = Header_error.GetAttribute("innerText");
            AssertTrue(UIerror.Equals(errormessage));
        }

        public  bool IsDialogPresent(IWebDriver remotedriver)
        {
            IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(remotedriver);
            return (alert != null);
        }

        public void SendLink(string emaildId,string reference=null)
        {
            //Common.DeleteInbox();
            //click on stream tab
            ClickOn(Link_StreamTab);

            //wwait
            

            // input email ID and reference
            Set_Textbox(Input_EmailBox, emaildId);
            if (String.IsNullOrEmpty(reference))
            {
                Set_Textbox(Input_Reference, "Automation");
            }
            else
            {
                Set_Textbox(Input_Reference, reference);
            }
            ClickOn(Button_Send);

        }

        public void AddStream(string emailId)
        {
            Wait(5);
            //click on Add Stream
            ClickOn(Button_AddStream);

            // set mailid
            Set_Textbox(Input_AddstreamMail, emailId);

            //click on send button
            ClickOn(Button_SendAddStream);
        }

        public void VerifyAwaitingLiveStreamPage()
        {
            //verify awaiting media
            if (IsElementDisplayedNEnabled(I_VideoLoader) && IsElementDisplayedNEnabled(P_AwaitingLiveStream))
            {
                Highlight(I_VideoLoader);
                Highlight(P_AwaitingLiveStream);
                ExtentReportUtil.report.Log(Status.Pass, "Element displayed & enabled");
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element Not displayed/enabled");
            }

        }

        public void EndStreaming()
        {
            //click on End Session button
            Thread.Sleep(30000);

            //click on End Session button
            ClickOn(Button_EndSession);

            if (IsDialogPresent(driver))
            {

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }

        }
     
        public void ViewSensitiveSessionAndVerifyActivtyPage()
        {
            //go to activities tab
            ClickOn(LoggingActivity.Link_ActivitiesTab);
            Wait(5);
            if (LoggingActivity.Allrows.Count <= 1)
            {
                //click on last 30 days from date range
                ClickOn(ReportsPage.Div_Date_Range_box);
                ClickOn(ActivitiesPage.Li_30days);
            }
            Thread.Sleep(3000);

            ClickOn(Span_ViewSensitiveSession);

            //verify sesnsitive entry
            if (IsElementDisplayedNEnabled(Td_SensitiveSessionLog))
            {
                Highlight(Td_SensitiveSessionLog);
                ExtentReportUtil.report.Log(Status.Pass, "Session marked as Sesnsitve log are present as expected");
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Fail, "Session marked as Sesnsitve log is missing");
            }
        }

        public void KMSKillingBrowser(string killer,string devicemode = null)
        {

            SetCapabilities();
            RemoteWebDriver(devicemode);
            MailRepository.ReadEmail("testuserbs007@gmail.com", "Password@123");
            remotedriver.Navigate().GoToUrl(MailRepository.FirstCaller.ElementAt(0));

            Thread.Sleep(1000);

            if (IsDialogPresent(remotedriver))
            {
                IAlert alert = remotedriver.SwitchTo().Alert();
                alert.Accept();
            }

            Thread.Sleep(3000);
            remotedriver.FindElement(By.CssSelector("div#home-buttons > div > button[data-i18n='startStreamButtonText']")).Click();
            Thread.Sleep(1000);
            // Logger.Message("Clicked on Start Button");
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }

            Thread.Sleep(5000);
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Thread.Sleep(1000);
            //VerifyStreamingStatus();
            if (killer.Equals("Operator"))
            {
                ExtentReportUtil.report.Log(Status.Info, "Operator killing browser:");
                driver.Close();
                ExtentReportUtil.report.Log(Status.Info, "Operator browser Killed.");
                driver.Quit();
                new Common().LaunchBrowser("CHROME");
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Info, "Caller killing browser:");
                remotedriver.Close();
                ExtentReportUtil.report.Log(Status.Info, "Caller browser Killed.");
                remotedriver.Quit();
                Wait(5);
            }
       }

        public void KMSSwitchApplication(string switcher, string devicemode = null)
        {
            SetCapabilities();
            RemoteWebDriver(devicemode);
            MailRepository.ReadEmail("testuserbs007@gmail.com", "Password@123");
            remotedriver.Navigate().GoToUrl(MailRepository.FirstCaller.ElementAt(0));

            Thread.Sleep(10000);

            if (IsDialogPresent(remotedriver))
            {
                IAlert alert = remotedriver.SwitchTo().Alert();
                alert.Accept();
            }

            Thread.Sleep(3000);
            remotedriver.FindElement(By.CssSelector("div#home-buttons > div > button[data-i18n='startStreamButtonText']")).Click();
            Thread.Sleep(10000);
            // Logger.Message("Clicked on Start Button");
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }

            Thread.Sleep(5000);
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Thread.Sleep(7000);
            if (switcher.Equals("Operator"))
            {
                ExtentReportUtil.report.Log(Status.Info, "Operator switches Application:");
                commoncls.LaunchBrowser("CHROME");
                driver.Navigate().GoToUrl("https://google.com");
                ExtentReportUtil.report.Log(Status.Info, "Operator Application switched:.");
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Info, "Caller switches Application:");
                remotedriver.Navigate().GoToUrl("https://google.com");
                ExtentReportUtil.report.Log(Status.Info, "Caller  Application Switched:");
                Wait(5);
            }
        }

        public void KMSReloadApplication(string reloader, string devicemode = null)
        {
            SetCapabilities();
            RemoteWebDriver(devicemode);
            MailRepository.ReadEmail("testuserbs007@gmail.com", "Password@123");
            remotedriver.Navigate().GoToUrl(MailRepository.FirstCaller.ElementAt(0));

            Thread.Sleep(10000);

            if (IsDialogPresent(remotedriver))
            {
                IAlert alert = remotedriver.SwitchTo().Alert();
                alert.Accept();
            }

            Thread.Sleep(3000);
            remotedriver.FindElement(By.CssSelector("div#home-buttons > div > button[data-i18n='startStreamButtonText']")).Click();
            Thread.Sleep(10000);
            // Logger.Message("Clicked on Start Button");
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }

            Thread.Sleep(5000);
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(10000);
            if (reloader.Equals("Operator"))
            {
                ExtentReportUtil.report.Log(Status.Info, "Operator refreshes streaming page:");
                driver.Navigate().Refresh();
                ExtentReportUtil.report.Log(Status.Info, "Operator streaming page refreshed.");
                Wait(5);
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Info, "Caller refreshes streaming page:");
                remotedriver.Navigate().Refresh();

                ExtentReportUtil.report.Log(Status.Info, "Caller streaming page refreshed.");
                Wait(5);
            }
        }

        public void KMSOperatorLogOff(string devicemode = null)
        {
            SetCapabilities();
            RemoteWebDriver(devicemode);
            MailRepository.ReadEmail("testuserbs007@gmail.com", "Password@123");
            remotedriver.Navigate().GoToUrl(MailRepository.FirstCaller.ElementAt(0));

            Thread.Sleep(10000);

            if (IsDialogPresent(remotedriver))
            {
                IAlert alert = remotedriver.SwitchTo().Alert();
                alert.Accept();
            }

            Thread.Sleep(3000);
            remotedriver.FindElement(By.CssSelector("div#home-buttons > div > button[data-i18n='startStreamButtonText']")).Click();
            Thread.Sleep(10000);
            // Logger.Message("Clicked on Start Button");
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }

            Thread.Sleep(5000);
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }
            remotedriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Thread.Sleep(7000);
            logging.KMSLogout();
        }

        public void VerifyKMSStatus()
        {
            try
            {
                //launching new browser with QA URL
                //if (driver == null)
                //{
                    commoncls.LaunchBrowser("CHROME");
               // }
                driver.Navigate().GoToUrl(GlobalVariables.URL);
                Wait(10);
                if (IsElementDisplayedNEnabled(Login_Header))
                {
                    string text = Login_Header.GetAttribute("innerHTML");
                    if (text.Equals("Log in"))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "KMS is up and running");
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "KMS is down/crashed:");
                }
                //driver.Close();

            }
            catch (Exception ex)
            {
                ExtentReportUtil.Test.Log(Status.Fail, "KMS is down/crashed: Environment InAccessible");
                driver.Close();
            }
        }

        public void VerifyStreamingStatus()
        {
            try {
                if (IsElementDisplayed(By.XPath("//p[text()='Awaiting Live Stream']")))
                {
                    ExtentReportUtil.report.Log(Status.Fail, "KMS is down/crashed:");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Pass, "KMS is up and running");
                }
                driver.Quit();

            }
            
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Pass, "KMS is up and running");
                driver.Quit();
            }
        }

        public void VerifyTomCatStatus()
        {
            //access URL

        }

        public void StartStreamingOnRealDevice(string devicemode = null)
        {

            SetCapabilities();
            RemoteWebDriver(devicemode);
            MailRepository.ReadEmail("testuserbs007@gmail.com", "Password@123");
            remotedriver.Navigate().GoToUrl(MailRepository.FirstCaller.ElementAt(0));

            Thread.Sleep(1000);

            if (IsDialogPresent(remotedriver))
            {

                IAlert alert = remotedriver.SwitchTo().Alert();
                alert.Accept();
            }

            Thread.Sleep(1000);

            remotedriver.FindElement(By.CssSelector("div#home-buttons > div > button[data-i18n='startStreamButtonText']")).Click();
            Thread.Sleep(1000);
            // Logger.Message("Clicked on Start Button");
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }
            Thread.Sleep(5000);
            if (IsDialogPresent(remotedriver))
            {
                IAlert alert1 = remotedriver.SwitchTo().Alert();
                alert1.Accept();
            }


            //  remotedriver.Quit();
        }

    }
}

