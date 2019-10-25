using System;
using System.Globalization;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;

namespace SeleniumFramework.Utilities.common
{
    class MailReport
    {
      //public static void SendMail()
      //  {
      //      MailAddress addressFrom = "automationresult58@gmail.com";
      //      MailAddress[] addressTo = { "yogesh.pawar@capita.co.uk",
      //                                    "madhuri.nichal@capita.co.uk" };
            
      //      MailMessage message = new MailMessage(addressFrom, addressTo);
      //      string msgbody = "Hello Team," +
      //                       " Please have a look at the attached Report for Smoke Suite execution on:" + DateTime.Now.ToString();
      //      message.Subject = "9Eye-Smoke Suite result";
      //      message.BodyText = msgbody;
      //      message.Date = DateTime.Now;
      //      message.Attachments.Add(new Attachment(GlobalVariables.ZipFolderpath+".zip"));
      //      // message.Attachments.Add(new Attachment(GlobalVariables.logReportFolder + "\\index.html"));
      //      //message.Attachments.Add(new Attachment(GlobalVariables.logReportFolder + "\\dashboard.html"));
      //      //message.Cc.Add(adressCC.Address);

      //      SmtpClient smtp = new SmtpClient();
      //      smtp.Host = "smtp.gmail.com";
      //      smtp.ConnectionProtocols = ConnectionProtocols.Ssl;
      //      smtp.Username = addressFrom.Address;
      //      smtp.Password = "Password@123";
      //      smtp.Port = 587;
      //      smtp.SendOne(message);

      //  }

        public static void SendMail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("EyeSmokeSuiteExecution", "automationresult58@gmail.com"));
            message.To.Add(new MailboxAddress("email_to", "yogesh.pawar@capita.co.uk"));
            //message.To.Add(new MailboxAddress("email_to", "robin.relan@capita.co.uk"));
            message.To.Add(new MailboxAddress("email_to", "mayank.tripathi@capita.co.uk"));
            message.Subject = "9Eye-Smoke Suite result";

            var builder = new BodyBuilder();

            builder.TextBody = "Hello Team," +
                             " Please have a look at the attached Report for Smoke Suite execution on: " + DateTime.Now.ToString();

            builder.Attachments.Add(GlobalVariables.ZipFolderpath + ".zip");

            message.Body = builder.ToMessageBody();

            try
            {
                var client = new SmtpClient();

                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("automationresult58@gmail.com", "Password@123");
                client.Send(message);
                client.Disconnect(true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Send Mail Failed : " + e.Message);
            }

        }
    }
}