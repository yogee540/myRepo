using AventStack.ExtentReports;
using OpenQA.Selenium;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.reportUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Pages.Day2
{
    class Reports : UIActions
    {

        public static IWebElement Link_ReportsTab => driver.FindElement(By.CssSelector("a#reports-tab"));
        public static IWebElement Select_ReportType => driver.FindElement(By.CssSelector("select#cardtype"));
        public static IWebElement Span_ReportType_Label => driver.FindElement(By.XPath("//span[text()='Report Type *']"));

        public static IWebElement H2_Report_Header => driver.FindElement(By.CssSelector("div.col-lg-10.col-md-10.col-sm-10.col-xs-10.col-lg-offset-1.col-md-offset-1.col-sm-offset-1.col-xs-offset-1 h2"));
        public static IWebElement Span_Org_Label => driver.FindElement(By.XPath("//div[@class='panel-body']/div/span[text()='Organisation']"));

        public static IWebElement Span_Date_Label => driver.FindElement(By.XPath("//div[@class='panel-body']/div/span[text()='Date *']"));

        public static IWebElement Label_Value_Org_admin => driver.FindElement(By.XPath("//label[@id='currentOrgnm']")); 

        public static IWebElement button_Generate_Report => driver.FindElement(By.CssSelector("button#generate-report-button"));

        public static IWebElement Div_Date_Range_box => driver.FindElement(By.CssSelector("div#reportrange"));

        public static  List<IWebElement> Li_Date_Ranges => driver.FindElements(By.XPath("//div[@class='ranges']/ul/li")).ToList();

        public static IWebElement Select_Value_Org_superuser => driver.FindElement(By.XPath("//select[@id='reportDatePicker']"));
        

        LoginPage login = new LoginPage();

        public void VerifyReportsUI()
        {
            ClickOn(Link_ReportsTab);
            if (IsElementDisplayedNEnabled(H2_Report_Header) && IsElementDisplayedNEnabled(Span_ReportType_Label) && IsElementDisplayedNEnabled(Select_ReportType))
            {
                Highlight(H2_Report_Header);
                Highlight(Span_ReportType_Label);
                Highlight(Select_ReportType);
                ExtentReportUtil.report.Log(Status.Pass, "Element displayed & enabled");
            }
            else {
                ExtentReportUtil.report.Log(Status.Fail, "Element Not displayed/enabled");
            }

        }

        public void VerifyReportInputs(string btnLabel, List<string> expRanges)
        {
            List<string> Act_Range = new List<string>();

            ClickOn(Link_ReportsTab);
            SelectFromDropDown(SelectionType.selectByValue, Select_ReportType, "Org_Usage");
           
            if (IsElementDisplayedNEnabled(Span_Org_Label) && IsElementDisplayedNEnabled(Span_Date_Label) && IsElementDisplayedNEnabled(button_Generate_Report) && IsElementDisplayedNEnabled(Div_Date_Range_box))
            {
                Wait(3);
                
                
                string btnText = GetLabelText(button_Generate_Report);
                AssertTrue(btnText.Equals(btnLabel));
                ClickOn(Div_Date_Range_box);
                foreach (IWebElement w in Li_Date_Ranges)
                {
                    string label = w.GetAttribute("innerText");
                    Act_Range.Add(label);

                }
                var diff = expRanges.Except(Act_Range);
                   
                ExtentReportUtil.report.Log(Status.Pass, "Element displayed & enabled");
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element Not displayed/enabled");
            }

        }


        public void VerifyUserSpecificElements(string user, String orgValue)
        {
            if (user.Contains("super")) {
                IsElementDisplayedNEnabled(Select_Value_Org_superuser);
            }
            else{
                string org = GetLabelText(Label_Value_Org_admin);
                AssertTrue(org.Equals(orgValue));

            }
            
        }
    }
}
