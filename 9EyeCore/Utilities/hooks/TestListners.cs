﻿using NUnit.Framework;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.common;
using SeleniumFramework.Utilities.reportUtil;
using SeleniumFramework.Utilities.XmlUtil;
using System.IO;

namespace SeleniumFramework.Utilities.hooks
{

    [TestFixture]
    class TestListners : UIActions
    {

        public static XMLTestDataReader testData;
       
        [SetUp]
        public void StartTest()
        {
            string className;
            className = TestContext.CurrentContext.Test.ClassName;
            className = className.Substring(className.LastIndexOf(".") + 1);
            string TestFullName = TestContext.CurrentContext.Test.FullName;
            string TestName = TestFullName.Substring(TestFullName.IndexOf(".", TestFullName.IndexOf(".") + 1) + 1);
            string TestDescription;
            try
            {
                TestDescription = TestContext.CurrentContext.Test.Properties.Get("Description").ToString();
                TestDescription = "<br><font size='1'>" + TestDescription + "</font>";
            }
            catch (System.Exception)
            {
                TestDescription = "";
            }
            ExtentReportUtil.Test = ExtentReportUtil.extent.CreateTest(TestName + " " + TestDescription);
            if (TestDescription.Contains("API"))
            {

            }
            else
            {
                new Common().LaunchBrowser(GlobalVariables.Browser)
                   .NavigateTo(GlobalVariables.URL);
            }
            testData = new XMLTestDataReader();
            //testData.SetXMLName(className);
            GlobalVariables.testCaseID = TestContext.CurrentContext.Test.MethodName;
        }

        [TearDown]
        public void EndTest()
        {
            if (driver != null)
            {
                GlobalVariables.driver.Quit();
            }
        }
    }

}