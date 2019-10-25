using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Linq;

namespace SeleniumFramework.Utilities.common
{
    
    public class MailRepository
    {
        public static List<string> FirstCaller;
        public static List<string> SecondCaller;

        public static void ReadEmail(string Emaild,string password)
        {
            FirstCaller = new List<string>();
            string emailUrl = string.Empty;
            using (var client = new ImapClient())
            {
                // accept all SSL certificates
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate(Emaild,password);

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadWrite);
                List<UniqueId> uids = inbox.Search(SearchQuery.BodyContains("follow")).ToList();
                uids.Reverse();
                //uids.OrderByDescending(a => a.CompareTo(uids.Last()));
                foreach (var uid in uids)
                {
                    var msg = inbox.GetMessage(uid);
                    if(msg.TextBody.Contains("follow"))
                    {
                        emailUrl = msg.TextBody;
                    }
                    break;
                }
                //var message = inbox.GetMessage(inbox.Count - 1);


                //emailUrl = message.TextBody;
                string text = Regex.Replace(emailUrl.ToString(),
  @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)",
  "<a target='_blank' href='$1'>$1</a>");

                var regex = new Regex("<a [^>]*href=(?:'(?<href>.*?)')|(?:\"(?<href>.*?)\")", RegexOptions.IgnoreCase);
                //var urls = regex.Matches(text).OfType<Match>().Select(m => m.Groups["href"].Value).SingleOrDefault();

                var doc = new HtmlDocument();
                doc.LoadHtml(text);
                var nodes = doc.DocumentNode.SelectNodes("a[@href]").ToList();
                string temp = nodes[0].Attributes["href"].Value;
                FirstCaller.Add(temp);
                int count = FirstCaller.Count();
                client.Disconnect(true);
            }
        }
    }
}