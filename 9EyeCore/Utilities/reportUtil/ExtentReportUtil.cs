
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.common;
using System;
using System.IO;


namespace SeleniumFramework.Utilities.reportUtil
{

    public class ExtentReportUtil : UIActions
    {
        public static AventStack.ExtentReports.ExtentReports extent;
        private static ExtentTest test;
        public static ExtentHtmlReporter htmlReporter;
        public static ExtentReportUtil report;
        public static ExtentTest Test { get => test; set => test = value; }

        public void StartReport()
        {
            report = new ExtentReportUtil();
            string logFolderName = "";
            string extentReportPath = "";
            logFolderName = "log_" + new Common().GetTimeStamp();
            GlobalVariables.logReportFolder = GlobalVariables.reportFolder + logFolderName;
            extentReportPath = GlobalVariables.logReportFolder + "\\TestRunReport.html";
            if (GlobalVariables.ScreenshotType.Equals("External"))
            {
                Directory.CreateDirectory(GlobalVariables.logReportFolder + "/screenshots");
            }
            extent = new AventStack.ExtentReports.ExtentReports();
            htmlReporter = new ExtentHtmlReporter(extentReportPath);
            extent.AddSystemInfo("Host Name", GlobalVariables.MachineName);
            extent.AddSystemInfo("Environment", GlobalVariables.Environment);
            extent.AddSystemInfo("User Name", GlobalVariables.LoggedInUser);

            htmlReporter.LoadConfig(GlobalVariables.projectPath + @"Utilities\reportUtil\extent-config.xml");
            extent.AttachReporter(htmlReporter);
        }

        public void Log(Status status, string stepDetail)
        {
            switch (status)
            {
                case Status.Pass:
                    Test.Log(status, stepDetail);
                    break;
                case Status.Fail:
                    stepDetail = "<font color = 'red'>" + stepDetail + "</font>";
                    Test.Log(status, stepDetail);
                    //TakeScreenshot();
                    try
                    {
                        Assert.Fail();
                    }
                    catch (System.Exception)
                    {

                    }

                    break;
                case Status.Fatal:
                    Test.Log(status, stepDetail);
                    TakeScreenshot();
                    Assert.Fail();
                    break;
                case Status.Error:
                    stepDetail = "<font color = 'red'>" + stepDetail + "</font>";
                    Test.Log(status, stepDetail);
                    TakeScreenshot();
                    Assert.Fail();
                    break;
                case Status.Warning:
                    Test.Log(status, stepDetail);
                    break;
                case Status.Info:
                    Test.Log(status, stepDetail);
                    break;
                case Status.Skip:
                    Test.Log(status, stepDetail);
                    break;
                case Status.Debug:
                    Test.Log(status, stepDetail);
                    break;
                default:
                    break;
            }
        }

        internal void Log(object fail, string v)
        {
            throw new NotImplementedException();
        }

        public void EndReport()
        {
            extent.Flush();
        }

    }
}
