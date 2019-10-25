using NUnit.Framework;
using SeleniumFramework.Pages;
using SeleniumFramework.Pages.Day1;
using SeleniumFramework.Utilities.hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Tests.Day1
{
    class SensitiveClassification : TestListners
    {
        LoginPage loginPage = new LoginPage();
        ActivitiesPage activitiesPage = new ActivitiesPage();

        //string Username_Operator = "mayank_operator";
       
        public string Username_supervisor;
        public string Password;
       
        public string ClassifySession;
        public string Reason;

        [Test, Description("Activities Page | Supervisor role | Session classified as Sensitive")]
        [Category("Data Independent")]
        //[Ignore("NewEnvironment")]
        public void TC_132435_1_ClssifySessionMedia()
        {
            Username_supervisor = testData.ReadData("Day1", "Username_supervisor");
            Password = testData.ReadData("Day1", "Password");
            ClassifySession = testData.ReadData("Day1", "ClassifySession");
            Reason = testData.ReadData("Day1", "Reason");

            loginPage.LoginToApplication(Username_supervisor, Password);
            activitiesPage.ClassifySession(Reason);
            loginPage.LogOffFromApplication();
        }

        [Test, Description("Activities Page | Supervisor role | Session de-classified as Sensitive")]
        [Category("Data Independent")]
        //[Ignore("NewEnvironment")]
        public void TC_132435_2_declasifySessionMedia()
        {
            Username_supervisor = testData.ReadData("Day1", "Username_supervisor");
            Password = testData.ReadData("Day1", "Password");
            ClassifySession = testData.ReadData("Day1", "ClassifySession");
            Reason = testData.ReadData("Day1", "Reason");

            loginPage.LoginToApplication(Username_supervisor, Password);
            activitiesPage.ClassifySession(Reason);
            loginPage.LogOffFromApplication();
        }
    }
}
