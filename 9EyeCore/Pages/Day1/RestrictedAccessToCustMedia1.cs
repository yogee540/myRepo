using OpenQA.Selenium;
using SeleniumFramework.UIOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumFramework.Utilities.reportUtil;
using AventStack.ExtentReports;

namespace SeleniumFramework.Pages.Day1
{
    class RestrictedAccessToCustMedia1 : UIActions
    {
        public static IWebElement Tab_Activities => driver.FindElement(By.CssSelector("#activities-tab"));
        public static IWebElement txt_Search => driver.FindElement(By.CssSelector(".form-control.input-sm"));
        public static IWebElement Calender_Arrow => driver.FindElement(By.XPath("//div[@id='reportrange']//b"));
        public static IWebElement Lnk_Calender_ThisMonth => driver.FindElement(By.XPath("//li[text()='This Month']"));
        public static IWebElement Btn_AddUser => driver.FindElement(By.CssSelector("#add-user-button"));

        LoginPage log_page = new LoginPage();
        UnsupportedBrowserMessage1 Unsup = new UnsupportedBrowserMessage1();

        public void Activities_MediaVew(String ReferenceNum, String SessionLink, String Session_Info, String Session_Details)
        {
            log_page.ClickOn(Tab_Activities);
            Wait(1000);
            IsElementDisplayedNEnabled(txt_Search);
            ExtentReportUtil.report.Log(Status.Pass, "Search field displayed successfully.");
            ClickOn(Calender_Arrow);
            ClickOn(Lnk_Calender_ThisMonth);
            Wait(3000);
            EnterText(txt_Search, ReferenceNum); //"SG123456789"
            Click_EventByData_N_Index(SessionLink);
            Check_EventByData(Session_Info);
            Check_EventByData(Session_Details);
        }

        public void SupportOperator_UsersTab(String ReferenceNum, String SessionLink, String Session_Info, String Session_Details)
        {
            log_page.ClickOn(Tab_Activities);
            Wait(1000);
            IsElementDisplayedNEnabled(txt_Search);
            ExtentReportUtil.report.Log(Status.Pass, "Search field displayed successfully.");
            ClickOn(Calender_Arrow);
            ClickOn(Lnk_Calender_ThisMonth);
            Wait(3000);
            EnterText(txt_Search, ReferenceNum); //"SG123456789"
            Click_EventByData_N_Index(SessionLink);
            Check_EventByData(Session_Info);
            Check_EventByData(Session_Details);
        }


    }
}
