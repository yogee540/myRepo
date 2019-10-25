using NUnit.Framework;
using SeleniumFramework.Utilities.hooks;
using SeleniumFramework.Pages.Day0;
using System.Threading.Tasks;

namespace SeleniumFramework.Tests.AAKMS
{
   
    class KMSTest : TestListners
    {
        VideoStreaming vstream = new VideoStreaming();
        LoggingActivity logging = new LoggingActivity();
        string username = "yogee_operator";
        string password = "Mpassword@123";
        string mail = "testuserbs007@gmail.com";

        [Test, Description("Scenario 1: Operator kills the browser while streaming.")]
        [Category("Data Dependent")]
        [Order(1)]
        //[Ignore("StreamingNotWorking")] 
        public void OperatorKillsBrowser()
        {
           // vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);
            vstream.KMSKillingBrowser("Operator");
            vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 2: Caller kills the browser while streaming. d")]
        [Category("Data Dependent")]
        [Order(2)]
        //[Ignore("StreamingNotWorking")] 
        public void CallerKillsBrowser()
        {
           // vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);
            vstream.KMSKillingBrowser("Caller");
            //vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 3: Operator Switch to other application in between while streaming.")]
        [Category("Data Dependent")]
        [Order(3)]
        //[Ignore("StreamingNotWorking")] 
        public void OperatorSwitchesApplication()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
            //vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);
            vstream.KMSSwitchApplication("Operator");
            vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 4: Caller Switch to other application while streaming.")]
        [Category("Data Dependent")]
        [Order(4)]
        //[Ignore("StreamingNotWorking")] 
        public void CallerSwitchesApplication()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
           // vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);
            vstream.KMSSwitchApplication("Caller");
           // vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 5: Operator reload the application while streaming.")]
        [Category("Data Dependent")]
        [Order(5)]
        //[Ignore("StreamingNotWorking")] 
        public void OperatorReloadsPage()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
            //vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);
            vstream.KMSReloadApplication("Operator");
            vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 6: Caller reload application while streaming.")]
        [Category("Data Dependent")]
        [Order(6)]
        //[Ignore("StreamingNotWorking")] 
        public void CallerReloadsPage()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
            //vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);
            vstream.KMSReloadApplication("Caller");
            vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 7: Operator logoff the application while streaming")]
        [Category("Data Dependent")]
        [Order(7)]
        //[Ignore("StreamingNotWorking")] 
        public void OperatorLogOff()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
           // vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);

            vstream.KMSOperatorLogOff();
            vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 8: Multi streaming - all caller upload the heavy media.")]
        [Category("Data Dependent")]
        [Order(8)]
        //[Ignore("StreamingNotWorking")] 
        public void HeavyMedia()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
           //vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);

            vstream.StartAndStopStreamingOnRealDevice1(null,true);
            vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 8: Multi streaming - all caller upload the heavy media.")]
        [Category("Data Dependent")]
        [Order(9)]
        //[Ignore("StreamingNotWorking")] 
        public void TomcatVerification()
        {
            
            logging.Login(username, password);
            logging.VerifyServiceAvailable();

        }


    }
}
