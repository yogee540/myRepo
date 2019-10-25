using NUnit.Framework;
using SeleniumFramework.BusinessFunctions;
using SeleniumFramework.Utilities.common;
using SeleniumFramework.Utilities.reportUtil;
using SeleniumFramework.Utilities.XmlUtil;
using System;
using System.Diagnostics;

namespace SeleniumFramework.Tests
{

    [SetUpFixture]
    class FixtureListener
    {
        ExtentReportUtil extentReport = new ExtentReportUtil();
        public static XmlReader xmlReader;
        //Recorder rec;
        public TestContext TestContext { get; set; }
        [OneTimeSetUp]
        public void SetUpEnvironment()
        {

            string clientSecret = TestContext.Parameters["CLIENT_SECRET"].ToString();
            int env1 = Convert.ToInt32(clientSecret);
            // Initializing Environment Configuration
            xmlReader = new XmlReader();
            xmlReader.LoadXML(GlobalVariables.EnvConfigurationPath);
            string execution = xmlReader.ReadTagValue("Execution");
            CommonFeatures cf = new CommonFeatures();
            
            //cf.ExtractPDF();
            if (execution.Equals("Server"))
            {
               //select environment as per env value
                switch (env1)
                {
                    case 1:
                        cf.Environment01();
                        break;

                    case 2:
                        cf.Environment02();
                        break;

                    case 3:
                        cf.Environment03();
                        break;

                    default:
                        cf.Environment01();
                        break;
                }
            }
            else
            {
                cf.Environment01();
            }

            GlobalVariables.Environment = xmlReader.ReadTagValue("Environment");
            GlobalVariables.Browser = xmlReader.ReadTagValue("Browser");
            GlobalVariables.ReportType = xmlReader.ReadTagValue("ReportType", "ReportConfiguration");
            GlobalVariables.ScreenshotType = xmlReader.ReadTagValue("ScreenshotType", "ReportConfiguration");
            GlobalVariables.VideoRecordingFlag = xmlReader.ReadTagValue("VideoRecording", "ReportConfiguration");

            //Initializing extent Report
            extentReport.StartReport();

            //Start video recording
            //if (GlobalVariables.VideoRecordingFlag.Equals("Yes"))
            //{
            //    // rec = new Recorder(new VideoRecorder(GlobalVariables.logReportFolder + "/VideoReport.mp4", 2, SharpAvi.KnownFourCCs.Codecs.MotionJpeg, 30));
            //}
            //Process.Start(GlobalVariables.CaffeinFile);
        }



        [OneTimeTearDown]
        public void TearDownEnvironment()
        {

            new Common().CloseProcess("chromedriver");
            extentReport.EndReport();
            new CommonFeatures().ZipFolder();
            //new Common().CloseProcess("Caffeine");
            MailReport.SendMail();

        }
    }

}