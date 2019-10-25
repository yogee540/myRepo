using AventStack.ExtentReports;
using OpenQA.Selenium;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.common;
using SeleniumFramework.Utilities.reportUtil;
using System;
using System.Collections.Generic;
using SeleniumFramework.BusinessFunctions;
using System.IO;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace SeleniumFramework.Pages
{
   public class ActivitiesPage : UIActions
   {
        CommonFeatures commonFeatures = new CommonFeatures();
        Common commonPage = new Common();

        //Activities Objects
        public static IWebElement Link_ViewSession => driver.FindElement(By.XPath("//table[@id='activities']//a[text()='1']"));
        public static IWebElement Link_ViewSession_All => driver.FindElement(By.XPath("//table[@id='activities']//a[text()='View Session (']"));
        public static IWebElement Toggle_ClassifySession => driver.FindElement(By.XPath("//div[@id='visual-sensitive']"));
        public static IWebElement Input_ClassifyReason => driver.FindElement(By.XPath("//div[@class='panel-body center-text']/textarea"));
        public static IWebElement Btn_ClassifySubmit => driver.FindElement(By.XPath("//div[@class='panel-body center-text']/button[text()='SUBMIT']"));
        public static IWebElement Btn_ClassifyCancel => driver.FindElement(By.XPath("//div[@class='panel-body center-text']/button[text()='CANCEL']"));
        public static IWebElement ClassifySession_ON => driver.FindElement(By.XPath("//span[@class='x-toggle-container large x-toggle-container-checked']/div[@id='visual-sensitive']"));
        public static IWebElement ClassifySession_OFF => driver.FindElement(By.XPath("//span[@class='x-toggle-container large']/div[@id='visual-sensitive']"));
        public static IWebElement SenstiveWarning => driver.FindElement(By.XPath("//span[@title='Sensitive']"));
        public static IWebElement Btn_ReturnToActivities => driver.FindElement(By.XPath("//button[@id='return-to-activities-list-button']"));
        public static IWebElement Link_Activity => driver.FindElement(By.XPath("//a[text()='Activity']"));
        public static IWebElement Msg_OrgCreationFailed => driver.FindElement(By.XPath("//td[text()='Organisation create failed: duplicate organisation']"));
        public static IWebElement Btn_DownloadAudit => driver.FindElement(By.XPath("//button[text()=' Download Audit PDF']"));
        public static IWebElement Calendar => driver.FindElement(By.XPath("//i[@class='glyphicon glyphicon-calendar fa fa-calendar']"));
        public static IWebElement Table_Activities => driver.FindElement(By.XPath("//table[@id='activities']"));
        public static IWebElement txt_Search => driver.FindElement(By.CssSelector(".form-control.input-sm"));
        public static IWebElement Lnk_Calender_ThisMonth => driver.FindElement(By.XPath("//li[text()='This Month']"));
        public static IWebElement Link_ActivitiesTab => driver.FindElement(By.CssSelector("a#activities-tab"));
        public static IWebElement Span_DateRange => driver.FindElement(By.CssSelector("div#reportrange span"));
        public static IWebElement Li_30days => driver.FindElement(By.XPath("//div[@class='ranges']/ul/li[@data-range-key='Last 30 Days']"));
        public static IWebElement Button_Apply => driver.FindElement(By.CssSelector("button.applyBtn.btn.btn-sm.btn-success"));
        public static IWebElement Td_ViewSession => driver.FindElement(By.XPath("//table[@id='activities']/tbody/tr[1]/td[5]"));    
        public static IWebElement Id_SessionID => driver.FindElement(By.XPath("//span[@title='Session Id']/following::span[1]"));
        public static IWebElement TableHeader_DateTime => driver.FindElement(By.XPath("//th[@class='sorting_asc'][text()='Date/Time']"));
        public static IWebElement Calender_Arrow => driver.FindElement(By.XPath("//div[@id='reportrange']//b"));
        public static IWebElement Lnk_Calender_CustomRange => driver.FindElement(By.XPath("//li[text()='Custom Range']"));
        public static IWebElement StartDate => driver.FindElement(By.Name("daterangepicker_start"));
        public static IWebElement EndDate => driver.FindElement(By.Name("daterangepicker_end"));
        public static IWebElement Btn_Apply_Calender => driver.FindElement(By.XPath("//button[@class='applyBtn btn btn-sm btn-success']"));
        public static IWebElement Media_Deleted_Msg => driver.FindElement(By.XPath("//*[contains(text(),'has been deleted')]"));
        public static IWebElement Btn_Media_DownloadPhoto => driver.FindElement(By.XPath("//button[text()='Download Photo']"));
        public static IWebElement Btn_Media_DownloadVideo => driver.FindElement(By.XPath("//button[text()='Download Video']"));
        public static IWebElement Tab_Users => driver.FindElement(By.CssSelector("#users-tab"));
        public static IWebElement DrpDwn_Organisation => driver.FindElement(By.XPath("//label[text()='Organisation:']//following-sibling::div//select"));
        public static IWebElement Warning_Msg => driver.FindElement(By.XPath("//*[contains(text(),'Support Operator is standalone role.It cannot exist in combination with any other role')]"));
        public static IWebElement Btn_Cancel => driver.FindElement(By.XPath("//*[text()='Cancel']"));
        public static IWebElement Edit_Link => driver.FindElement(By.XPath("//*[text()='Edit']"));
        IWebElement NoRecords_Msg => driver.FindElement(By.XPath("//*[contains(text(),No matching records found')]"));
        IWebElement Tab_Activities => driver.FindElement(By.XPath("//a[@id='activities-tab']"));
        public static IWebElement Span_ViewSessionForSensitiveData => driver.FindElement(By.XPath("//span[@title='Sensitive']/preceding-sibling::span"));

        //View Session Activities Objects
        public static IWebElement Div_VideoThumbnail => driver.FindElement(By.XPath("//div[@class='video-thumbnail']"));
        public static IWebElement Btn_VideoDownloadl => driver.FindElement(By.XPath("//button[text()='Download Video']"));


        public static string SensitiveWarning_XPath = "//span[@title='Sensitive']";
        public static IWebElement Input_Search => driver.FindElement(By.XPath("//input[@type='search']"));
        
        //Wait Object on Activities Page
        public static IWebElement Pagination_Next => driver.FindElement(By.Id("activities_next"));
        public static string sysUsername = System.Environment.UserName;

        public bool WaitForActivitiesPageToLoad()
        {
            bool flag = false;

            WebDriverWait Wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("activities_next")));
                for (int i = 0; i < 100; i++)
                {
                    if (Pagination_Next.Displayed)
                    {
                        flag = true;
                        ExtentReportUtil.report.Log(Status.Pass, "Activities Page Loaded");
                        break;
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception Occurred: " + ex.ToString());
            }

            return flag;
        }

        public void ClassifySession(string Resaon)
        {
            try
            {
                Type t = SensitiveWarning_XPath.GetType();
                string name = t.Name;
                commonFeatures.ClickTab("Activities");
                Wait(7);
                WaitForActivitiesPageToLoad();
                SelectDateRange_Activities("Last 30 Days");
                Wait(7);
                WaitForActivitiesPageToLoad();
                commonFeatures.Search("View Session (1)");
                commonFeatures.ClickViewSession();
                Wait(10);
                ClickOn(Toggle_ClassifySession);
                Wait(1);
                EnterText(Input_ClassifyReason, Resaon);
                ClickOn(Btn_ClassifySubmit);
                VerifySessionClassified();
                ClickReturnToActivities();
                WaitForActivitiesPageToLoad();
                commonFeatures.Search("View Session (1)");
            }
            
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Toggle Not working.");
            }
        }

        public void VerifySessionClassified()
        {
            try
            {
                if (ClassifySession_ON.Displayed)
                {
                    ExtentReportUtil.report.Log(Status.Info, "Session Classified.");
                    TakeScreenshot();
                }
                Wait(2);                
            }
            catch (Exception)
            {
                if (ClassifySession_OFF.Displayed)
                {
                    ExtentReportUtil.report.Log(Status.Info, "Session Declassified.");
                    TakeScreenshot();
                }
            }
        }

        public void ClickReturnToActivities()
        {
            try
            {
                ClickOn(Btn_ReturnToActivities);
                WaitForActivitiesPageToLoad();

            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occurred in ClickReturnToActivities() method -- " + ex.ToString());
                TakeScreenshot();
            }
          
        }
        
        public void VerifyUserUpdatedInActivity(string UpdatedUserName)
        {
            By UserCreated_XPath = By.XPath("//td[text()='User updated: [" + UpdatedUserName + "]']");
            try
            {
                ClickOn(Link_Activity);
                Wait(2);
                //WaitForActivitiesPageToLoad();
                SortActivityTableWithDateDesc();
                if (driver.FindElement(UserCreated_XPath).Displayed)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "User Updated and displayed in activity -- " + UpdatedUserName);
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Error, "Exception Occurred: " + ex.ToString());
            }
        }

        public void VerifyDownloadAuditPDFButton()
        {
            try
            {
                ClickOn(Link_ViewSession_All);
                if (Btn_DownloadAudit.Displayed)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Download Audit PDF is displayed");
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception Occurred: " + ex.ToString());
            }
        }

        public void SelectDateRange_Activities(string DateRange)
        {

            string DateRange_XPath = "//div[@class='ranges']//li[text()='" + DateRange + "']";
            try
            {
                if (Calendar.Displayed)
                {
                    WaitForActivitiesPageToLoad();
                    ClickOn(Calendar);
                    ClickOn(driver.FindElement(By.XPath(DateRange_XPath)));
                    Wait(10);
                    WaitForActivitiesPageToLoad();
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception Occurred: " + ex.ToString());
            }
        }  

        public void SortActivityTableWithDateDesc()
        {
            ClickOn(TableHeader_DateTime);
        }

        public void VerifyTextInPDF(string FileName)
        {
            string pdfFile = "C:\\Users\\" + sysUsername + "\\Downloads\\" + FileName;
            Wait(30);
            ClickOn(Link_ViewSession_All);
            Wait(2);
            ClickOn(Btn_DownloadAudit);
            string sessionID = GetSessionIDFromURL();
            //string sessionID = Id_SessionID.GetAttribute("innerText");
            Wait(10);
            try
            {
                if (commonFeatures.ExtractTextFromPDF(pdfFile).Contains(sessionID))
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Session ID -- " + sessionID + " matches on UI and PDF");
                }

            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Error, "Exception Occurred: " + ex.ToString());
            }
        }

        private string GetSessionIDFromURL()
        {
            string[] a = driver.Url.Split("/");
            return a[5];
        }

    }
}
