﻿using NUnit.Framework;
using SeleniumFramework.Utilities.hooks;
using SeleniumFramework.Pages;
using SeleniumFramework.BusinessFunctions;

namespace SeleniumFramework.Tests.Day2
{
    class DownloadAuditPDF : TestListners
    {
        CommonFeatures commonFeatures = new CommonFeatures();
        LoginPage loginPage = new LoginPage();
        ActivitiesPage activitiesPage = new ActivitiesPage();

        public string Username_superadmin = string.Empty;
        public string Username_admin = string.Empty;
        public string Username_supervisor = string.Empty;
        public string Username_operator = string.Empty;
        public string Username_supportoperator = string.Empty;
        public string Username_superoperator = string.Empty;
        public string Password = string.Empty;
        public string Password1 = string.Empty;
        //for TC 119529
        public string username = string.Empty;
        public string password = string.Empty;
        public string pdfName = string.Empty;
        public string Tab_Activities = string.Empty;
        public string SelectDate = string.Empty;
        string opassword = string.Empty;


        [Test, Description("Download AuditPDF button")]
        [Category("Data Independent")]
        //[Ignore("NewEnvironment")]
        public void TC_132437_1_VerifyDownloadAuditPDFButton()
        {
            Tab_Activities = testData.ReadData("Day2", "Tab_Activities");
            SelectDate = testData.ReadData("Day2", "SelectDate");
            Username_superadmin = testData.ReadData("Day2", "Username_superadmin");
            Username_admin = testData.ReadData("Day2", "Username_admin");
            Username_supervisor = testData.ReadData("Day2", "Username_supervisor");
            Username_operator = testData.ReadData("Day2", "Username_operator");
            Username_supportoperator = testData.ReadData("Day2", "Username_supportoperator");
            Password = testData.ReadData("Day2", "Password");
            password = testData.ReadData("Day2", "password");
            opassword = testData.ReadData("Day2", "opassword");


            //loginPage.LoginToApplication(Username_superadmin, Password);
            //commonFeatures.ClickTab(Tab_Activities);
            //activitiesPage.VerifyDownloadAuditPDFButton();
            //loginPage.LogOffFromApplication();

            loginPage.LoginToApplication(Username_admin, Password);
            commonFeatures.ClickTab(Tab_Activities);
            activitiesPage.VerifyDownloadAuditPDFButton();
            loginPage.LogOffFromApplication();

            loginPage.LoginToApplication(Username_supervisor, Password);
            commonFeatures.ClickTab(Tab_Activities);
            activitiesPage.VerifyDownloadAuditPDFButton();
            loginPage.LogOffFromApplication();

            loginPage.LoginToApplication(Username_operator, opassword);
            commonFeatures.ClickTab(Tab_Activities);
            activitiesPage.SelectDateRange_Activities(SelectDate);
            activitiesPage.VerifyDownloadAuditPDFButton();
            loginPage.LogOffFromApplication();

            //loginPage.LoginToApplication(Username_supportoperator, Password);
            //commonFeatures.ClickTab(Tab_Activities);
            //Wait(30);
            //activitiesPage.VerifyDownloadAuditPDFButton();
            //loginPage.LogOffFromApplication();
        }

        [Test, Description("Download Audit PDF | Activity log success")]
        [Category("Data Independent")]
        //[Ignore("NewEnvironment")]
        public void TC_132437_2_VerifyDownloadAuditPDFButtonForUserRoles()
        {
            Username_admin = testData.ReadData("Day2", "Username_admin");
            Username_superoperator = testData.ReadData("Day2", "Username_superoperator");
            Password = testData.ReadData("Day2", "Password");
            pdfName = testData.ReadData("Day2", "PDFName");
            Tab_Activities = testData.ReadData("Day2", "Tab_Activities");

            loginPage.LoginToApplication(Username_admin, Password);
            commonFeatures.ClickTab(Tab_Activities);
            activitiesPage.VerifyTextInPDF(pdfName);
            //commonFeatures.DeleteDownloadedFile(pdfName);
            loginPage.LogOffFromApplication();

            //loginPage.LoginToApplication(Username_superoperator, Password);
            //commonFeatures.ClickTab(Tab_Activities);
            //activitiesPage.VerifyTextInPDF(pdfName);
            //commonFeatures.DeleteDownloadedFile(pdfName);
            //loginPage.LogOffFromApplication();
        }
    }
}
