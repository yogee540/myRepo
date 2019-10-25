using NUnit.Framework;
using SeleniumFramework.Utilities.hooks;
using SeleniumFramework.Pages.Day0;
using System.Threading.Tasks;


namespace SeleniumFramework.Tests.AAKMS
{
    //[TestFixture("parallel", "chrome")]
    //[TestFixture("parallel", "safari")]
    //[TestFixture("parallel", "ie")]
    //[TestFixture("parallel", "firefox")]
    //[Parallelizable(ParallelScope.Fixtures)]

    class MultipleUser : TestListners
    {
        //public MultipleUser(string profile, string environment)
        //         : base(profile, environment) { }

        VideoStreaming vstream = new VideoStreaming();
        LoggingActivity logging = new LoggingActivity();
        string username = "yogee_operator";
        string password = "Mpassword@123";
        string mail = "testuserbs007@gmail.com";

        [Test, Description("Scenario 9: Multiple devices (more than 10) access the same link sent by an Operator.")]
        [Category("Data Dependent")]
        [Order(12)]
        //[Ignore("StreamingNotWorking")] 
        public void Multipleusers1()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
            //vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);

            vstream.StartAndStopStreamingOnRealDevice1();
            vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 9: Multiple devices (more than 10) access the same link sent by an Operator.")]
        [Category("Data Dependent")]
        public void Multipleusers2()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
            //vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);


            vstream.StartAndStopStreamingOnRealDevice1();
            vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 10: Multi streaming - all caller upload the heavy media.")]
        [Category("Data Dependent")]
        [Order(16)]
        public void MultipleuserGracePeriodHeavyMedia1()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
            //vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);
            vstream.EndStreaming();

           // vstream.StartAndStopStreamingOnRealDevice1(null, true);
            vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 10: Multi streaming - all caller upload the heavy media.")]
        [Category("Data Dependent")]
        [Order(15)]
        public void MultipleuserGracePeriodHeavyMedia2()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
            //vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);
            vstream.EndStreaming();

            //vstream.StartAndStopStreamingOnRealDevice1(null, true);
                                
            vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 11: Multiple devices (more than 10) access the same link sent by an Operator.GracePeriod")]
        [Category("Data Dependent")]
        [Order(13)]
        //[Ignore("StreamingNotWorking")] 
        public void MultipleuserGracePeriod1()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
            //vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);
            vstream.EndStreaming();

            //Parallel.Invoke(() => vstream.GracePeriodTakePhoto(),
            //                   () => vstream.GracePeriodTakePhoto());
            //vstream.GracePeriodTakePhoto();
            vstream.VerifyKMSStatus();
        }

        [Test, Description("Scenario 11: Multiple devices (more than 10) access the same link sent by an Operator.GracePeriod")]
        [Category("Data Dependent")]
        [Order(14)]
        //[Ignore("StreamingNotWorking")] 
        public void MultipleuserGracePeriod2()
        {
            //string username = testData.ReadData("KMSTests", "username");
            //string password = testData.ReadData("KMSTests", "password");
            //string mail = testData.ReadData("KMSTests", "mail");
            //vstream.VerifyKMSStatus();
            logging.Login(username, password);
            vstream.SendLink(mail);
            vstream.EndStreaming();
            //vstream.GracePeriodTakePhoto();
            vstream.VerifyKMSStatus();

       }

        [Test, Description("Scenario 12: Multiple devices (more than 10) access the same link sent by an Operator.ExpiredLink")]
        [Category("Data Dependent")]
        [Order(11)]
        //[Ignore("StreamingNotWorking")] 
        public void MultipleuserExpiredLink1()
        {
            
            logging.Login(username, password);

            vstream.StartAndStopStreamingOnRealDeviceExpiredLink();
            
            vstream.VerifyKMSStatus();
        }

        //[Test, Description("Scenario 12: Multiple devices (more than 10) access the same link sent by an Operator.ExpiredLink")]
        //[Category("Data Dependent")]
        ////[Ignore("StreamingNotWorking")] 
        //public void MultipleuserExpiredLink2()
        //{
        //    //string username = testData.ReadData("KMSTests", "username");
        //    //string password = testData.ReadData("KMSTests", "password");
        //    //string mail = testData.ReadData("KMSTests", "mail");
        //    //vstream.VerifyKMSStatus();
        //    logging.Login(username, password);
        
           
        //   vstream.StartAndStopStreamingOnRealDeviceExpiredLink();
        //    vstream.VerifyKMSStatus();
        //}

    }
}
