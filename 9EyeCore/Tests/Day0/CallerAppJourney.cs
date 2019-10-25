﻿using NUnit.Framework;
using SeleniumFramework.Utilities.hooks;
using SeleniumFramework.Pages.Day0;
using System.Threading.Tasks;

namespace SeleniumFramework.Tests.Day0
{
    class CallerAppJourney : TestListners
    {
        VideoStreaming vstream = new VideoStreaming();
        LoggingActivity logging = new LoggingActivity();

        [Test, Description("Web RTC Enabled: Caller uploads media and operator ends the session ")]
        [Category("Data Dependent")]
        //[Ignore("StreamingNotWorking")] 
        public void TC_132433_StreamingSerivce()
        {
            string username = testData.ReadData("Day0", "username");
            string password = testData.ReadData("Day0", "password");
            string mail = testData.ReadData("Day0", "mail");

            logging.Login(username, password);
            vstream.SendLink(mail);
            vstream.StartAndStopStreamingOnRealDevice1();
            vstream.EndStreaming();
        }

        //[Test, Description("Web RTC Enabled: Multistreaming ")]
        //[Category("Data Dependent")]
        ////[Ignore("StreamingNotWorking")] 
        //public void TC_132433_StreamingSerivce_1()
        //{
        //    string username = testData.ReadData("Day0", "Username");
        //    string password = testData.ReadData("Day0", "Password");
        //    string mail = testData.ReadData("Day0", "mail");
        //    int j = 0;

        //    logging.Login(username, password);
        //    vstream.SendLink(mail);
        //    vstream.StartStreamingOnRealDevice();

        //    new Task(vstream.Findstopbutton).Start();

        //    while (j <= 2)
        //    {
        //        new LoggingActivity().clickaddstream().Enteremail(mail).clickonsendbutton();

        //        vstream.StartStreamingOnRealDevice();
        //        new Task(vstream.Findstopbutton).Start();
        //        j++;
        //    }

        //}
    }
}