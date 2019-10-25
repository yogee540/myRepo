﻿using NUnit.Framework;
using SeleniumFramework.BusinessFunctions;
using SeleniumFramework.Pages;
using SeleniumFramework.Pages.Day0;
using SeleniumFramework.Pages.Day2;
using SeleniumFramework.Utilities.common;
using SeleniumFramework.Utilities.hooks;
using System;
using System.Collections.Generic;

namespace SeleniumFramework.Tests.Day2
{
    class DeleteOrganisation : TestListners
    {
        LoginPage loginPage = new LoginPage();
        OrganisationPage orgPage = new OrganisationPage();
        CommonFeatures commonFeatures = new CommonFeatures();
        UsersPage usersPage = new UsersPage();

        string username = string.Empty;
        string password = string.Empty;
        string organisationName = string.Empty;
        private string username1 = string.Empty;
        private string password1 = string.Empty;
        private string username2 = string.Empty;
        private string password2 = string.Empty;
        private string valueOfOrg = string.Empty;
        private string btnLabel = string.Empty;



        [Test, Description("Delete organisation success")]
        [Category("Data Dependent")]
        //Ignore("NewEnvironment")]
        public void TC_132569()
        {
            username = testData.ReadData("Day2", "Username");
            password = testData.ReadData("Day2", "Password");
            organisationName = testData.ReadData("Day2", "OrganisationName");

            loginPage.LoginToApplication(username, password);
            Assert.True(orgPage.DeleteOrganisation(organisationName), "Unable to delete Organisation: " + organisationName);
            commonFeatures.ClickTab("Users");
            Wait(5);
            commonFeatures.Search(username);
            usersPage.VerifyLogForDeletedOrganisation(username, organisationName);
            loginPage.LogOffFromApplication();
        }
    }
}