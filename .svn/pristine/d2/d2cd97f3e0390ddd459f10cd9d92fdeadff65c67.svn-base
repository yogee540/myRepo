using AventStack.ExtentReports;
using OpenQA.Selenium;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.common;
using SeleniumFramework.Pages;
using SeleniumFramework.Pages.Day1;
using SeleniumFramework.Utilities.reportUtil;
using System;
using System.Collections.Generic;

namespace SeleniumFramework.Pages
{
    class HelpPage : UIActions
    {

        //Help Objects
        public static IWebElement HelpIcon => driver.FindElement(By.XPath("//i[@class='fa fa-question-circle-o menu-toggle']"));
        public static IWebElement UserGuide_Tab => driver.FindElement(By.XPath("//a[text()='User Guide']"));
        public static IWebElement HelpQues_Classify_Declassify => driver.FindElement(By.XPath("//a[@class='faq-link'][text()='10. How do I classify/de-classify a session as sensitive?']"));
        public static IWebElement Btn_CloseHelp => driver.FindElement(By.XPath("//i[@class='fa fa-times menu-close']"));


        public HelpPage ClickHelp()
        {
            ClickOn(HelpIcon);
            WaitForObject(UserGuide_Tab, 30);
            return new HelpPage();
        }

        public HelpPage ClickUserGuide()
        {
            ClickOn(UserGuide_Tab);
            WaitForObject(HelpQues_Classify_Declassify, 30);
            return new HelpPage();
        }

        public HelpPage ClickHelpQuestion()
        {
            ClickOn(HelpQues_Classify_Declassify);
            return new HelpPage();
        }

        public HelpPage CloseHelp()
        {
            ClickOn(Btn_CloseHelp);
            return new HelpPage();
        }
    }
}
