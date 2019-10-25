using AventStack.ExtentReports;
using OpenQA.Selenium;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.reportUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SeleniumFramework.Pages;

namespace SeleniumFramework.Pages.Day2
{
    class ReportsPage : UIActions
    {

        public static IWebElement Link_ReportsTab => driver.FindElement(By.CssSelector("a#reports-tab"));
        public static IWebElement Select_ReportType => driver.FindElement(By.CssSelector("select#cardtype"));
        public static IWebElement Span_ReportType_Label => driver.FindElement(By.XPath("//span[text()='Report Type *']"));

        public static IWebElement H2_Report_Header => driver.FindElement(By.CssSelector("div.col-lg-10.col-md-10.col-sm-10.col-xs-10.col-lg-offset-1.col-md-offset-1.col-sm-offset-1.col-xs-offset-1 h2"));
        public static IWebElement Span_Org_Label => driver.FindElement(By.XPath("//div[@class='panel-body']/div/span[text()='Organisation']"));

        public static IWebElement Span_Date_Label => driver.FindElement(By.XPath("//div[@class='panel-body']/div/span[text()='Date *']"));

        public static IWebElement Label_Value_Org_admin => driver.FindElement(By.XPath("//label[@id='currentOrgnm']"));

        public static IWebElement Button_Generate_Report => driver.FindElement(By.CssSelector("button#generate-report-button"));

        public static IWebElement Div_Date_Range_box => driver.FindElement(By.CssSelector("div#reportrange"));

        public static List<IWebElement> Li_Date_Ranges => driver.FindElements(By.XPath("//div[@class='ranges']/ul/li")).ToList();

        public static IWebElement Select_Value_Org_superuser => driver.FindElement(By.XPath("//select[@id='reportDatePicker']"));

        public static IWebElement Select_Org => driver.FindElement(By.CssSelector("select#reportDatePicker"));
        public static IWebElement Li_DateRange_Today => driver.FindElement(By.XPath("//li[text()='Today']"));

        public static IWebElement H2_ReportResull_Title => driver.FindElement(By.XPath("//h2[@class='display-inline-block margin-bottom-10px']"));

        public static IWebElement Button_BackToReport => driver.FindElement(By.XPath("//button[@id='return-to-report-list-button']"));
        public static IWebElement Div_ReportDateRange_ResultPage => driver.FindElement(By.XPath("//div[@id='reportrange1']"));

        public static IWebElement H4_DownloadLabel_ResultPage => driver.FindElement(By.XPath("//h4[text() = 'Download: ']"));

        public static IWebElement Button_DownloadPDF => driver.FindElement(By.XPath("//button[text()=' PDF']"));

        public static IWebElement Button_DownloadExcel => driver.FindElement(By.XPath("//button[text()=' Excel']"));

        public static IWebElement H3_SummaryTableHeader => driver.FindElement(By.CssSelector("table#report-summary-table1 h3"));

        public static List<IWebElement> Td_SummaryFieldLabels => driver.FindElements(By.XPath("//table[@id='report-summary-table']/tr/td[1]")).ToList();
        public static List<IWebElement> Th_ReportGrid_Columns => driver.FindElements(By.CssSelector("table#report thead tr th")).ToList();
        public static IWebElement Label_SearchFilter_ReportResult => driver.FindElement(By.XPath("//div[@id='report_filter']/label"));
        public static IWebElement Input_Searchbox_ReportResult => driver.FindElement(By.XPath("//input[@type='search']"));
        public static IWebElement H2_ResultPage_AllOrg => driver.FindElement(By.CssSelector("h2.display-inline-block.margin-bottom-10px"));
        public static IWebElement H3_ResultPage_AllOrg => driver.FindElement(By.CssSelector("h3.margin-bottom-10px"));
        public static List<IWebElement> Th_ReportGrid_Columns_AllOrg => driver.FindElements(By.CssSelector("table#reportAll thead tr th")).ToList();

        string label = string.Empty;

        List<string> temp_List;

        public void VerifyGeneratedReportSingleOrg(string org, string title, List<string> fieldList, List<string> columnlist)
        {
            List<string> UIFieldList = new List<string>();

            try
            {
                //Generate report for single org with superuser
                ClickOn(Link_ReportsTab);
                SelectFromDropDown(SelectionType.selectByValue, Select_ReportType, "Org_Usage");
                Wait(2);
                SelectFromDropDown(SelectionType.selectByText, Select_Org, org);
                ClickOn(Div_Date_Range_box);
                ClickOn(Li_DateRange_Today);
                ClickOn(Button_Generate_Report);
                Wait(2);
                //verify header
                if (IsElementDisplayedNEnabled(H2_ReportResull_Title))
                {
                    string PageTitle = GetLabelText(H2_ReportResull_Title);
                    if (PageTitle.StartsWith(title) && PageTitle.Contains(org))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Header displayed & contains selected organization");
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Header is incorrect");
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Header is missing");
                }

                //Verify back to report button, Date box(Disabled) , Download: PDF & Excel Buttons

                VerifyReportResultPage();

                //temp list for below loops
                temp_List = new List<String>();

                //Verify Summary Section

                foreach (IWebElement w in Td_SummaryFieldLabels)
                {
                    label = w.GetAttribute("innerText");
                    temp_List.Add(label);

                }

                var difference1 = temp_List.Except(fieldList);
                if (difference1.Count() == 0)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Fields are present as expected");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Fields are missing");
                }
                temp_List.Clear();

                //Verify Session Details & search Box
                IsElementDisplayedNEnabled(H3_SummaryTableHeader);
                string Searchlabel = GetLabelText(Label_SearchFilter_ReportResult);
                if (Searchlabel.Equals("Search:") && IsElementDisplayedNEnabled(Input_Searchbox_ReportResult))
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Search Element label/Input box available");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Search Element label/Input box missing");
                }
                foreach (IWebElement w in Th_ReportGrid_Columns)
                {
                    label = w.GetAttribute("innerText");
                    temp_List.Add(label);
                }
                var difference2 = temp_List.Except(columnlist);
                if (difference2.Count() == 0)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Columns are present as expected");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Columns are missing");
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log( Status.Debug,"Exception Occured"+ex.ToString());
            }
        }

        //Verify back to report button, Date box(Disabled) , Download: PDF & Excel Buttons
        public void VerifyReportResultPage()
        {
            if (IsElementDisplayedNEnabled(Button_BackToReport) &&
               IsElementDisplayedNEnabled(Div_ReportDateRange_ResultPage) &&
               IsElementDisplayedNEnabled(H4_DownloadLabel_ResultPage) &&
               IsElementDisplayedNEnabled(Button_DownloadPDF) &&
               IsElementDisplayedNEnabled(Button_DownloadExcel))
            {
                ExtentReportUtil.report.Log(Status.Pass, "All Elements are disaplayed right");
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element(s) is/are not visible/enabled");
            }
        }

        public void DownloadAndVerifyFiles(string pdf, string excel)
        {
            ClickOnJS(Button_DownloadPDF);
            Wait(1);
            ClickOnJS(Button_DownloadExcel);
            Wait(5);
            string pdfFile = "C:\\Users\\" + ActivitiesPage.sysUsername + "\\Downloads\\" + pdf;
            if (File.Exists(pdfFile))
            {
                ExtentReportUtil.report.Log(Status.Pass, "PDF downloaded to local");
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Fail, "PDF not found");
            }
            string excelFile = "C:\\Users\\" + ActivitiesPage.sysUsername + "\\Downloads\\" + excel;
            if (File.Exists(excelFile))
            {
                ExtentReportUtil.report.Log(Status.Pass, "Excel downloaded to local");
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Fail, "Excel not found");
            }
        }

    }
}

