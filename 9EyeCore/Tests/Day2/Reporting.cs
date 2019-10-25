using NUnit.Framework;
using SeleniumFramework.BusinessFunctions;
using SeleniumFramework.Pages;
using SeleniumFramework.Pages.Day2;
using SeleniumFramework.Utilities.hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SeleniumFramework.Tests.Day2
{
    class Reporting : TestListners
    {
        LoginPage loginPage = new LoginPage();
        OrganisationPage orgPage = new OrganisationPage();
        CommonFeatures common = new CommonFeatures();
        ReportsPage reports = new ReportsPage();
        string username = string.Empty;
        string password = string.Empty;
        string organisationName = string.Empty;
        private string valueOfOrg = string.Empty;
        private string btnLabel = string.Empty;
        private List<string> listRange;
        private string title = string.Empty;
        private List<string> FieldList;
        List<string> ColumnList;
        List<string> ColumnListAllOrg;
        private string header1 = string.Empty;
        private string header2 = string.Empty;



        [Test, Description("Verify Generated Report for single organization")]
        [Category("Data Dependent")]
        //[Ignore("NewEnvironment")]
        public void TC_132438_VerifyGeneratedReportForSingleOrg()
        {
            username = testData.ReadData("Day2", "Username");
            password = testData.ReadData("Day2", "Password");
            valueOfOrg = testData.ReadData("Day2", "orgValue");
            title = testData.ReadData("Day2", "header");
            ColumnList = new List<string>();
            FieldList = new List<string>();
            FieldList.Add(testData.ReadData("Day2", "field1"));
            FieldList.Add(testData.ReadData("Day2", "field2"));
            FieldList.Add(testData.ReadData("Day2", "field3"));
            FieldList.Add(testData.ReadData("Day2", "field4"));
            FieldList.Add(testData.ReadData("Day2", "field5"));
            FieldList.Add(testData.ReadData("Day2", "field6"));
            FieldList.Add(testData.ReadData("Day2", "field7"));
            FieldList.Add(testData.ReadData("Day2", "field8"));
            FieldList.Add(testData.ReadData("Day2", "field9"));

            ColumnList.Add(testData.ReadData("Day2", "column1"));
            ColumnList.Add(testData.ReadData("Day2", "column2"));
            ColumnList.Add(testData.ReadData("Day2", "column3"));
            ColumnList.Add(testData.ReadData("Day2", "column4"));
            ColumnList.Add(testData.ReadData("Day2", "column5"));
            ColumnList.Add(testData.ReadData("Day2", "column6"));
            ColumnList.Add(testData.ReadData("Day2", "column7"));
            ColumnList.Add(testData.ReadData("Day2", "column8"));


            loginPage.LoginToApplication(username, password);

            reports.VerifyGeneratedReportSingleOrg(valueOfOrg, title, FieldList, ColumnList);
        }

        //=================================================================================================================================================

    }
}
