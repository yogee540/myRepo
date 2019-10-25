using AventStack.ExtentReports;
using OpenQA.Selenium;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.reportUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Pages.Day1
{
    class ConfigurableLandingPage1 : UIActions
    {
        IWebElement Link_Edit => driver.FindElement(By.XPath("//a[text()='Edit']"));
        IWebElement Input_Search => driver.FindElement(By.XPath("//input[@type='search']"));
        IWebElement Header_OperatorGuidance => driver.FindElement(By.XPath("//span[@id='opGuidance']//p"));
        IWebElement Link_OperatorBranding => driver.FindElement(By.XPath("//a[contains(text(),'Operator Branding')]"));
        IWebElement Textarea_OperatorGuidance => driver.FindElement(By.XPath("//span[text()='Operator guidance HTML']//following-sibling::textarea"));
        IWebElement Btn_Save => driver.FindElement(By.XPath("//button[text()='Save']"));
        IWebElement ToolTip => driver.FindElement(By.XPath("//div[@class='tooltip-inner']"));
        IWebElement HelpIcon_GuidanceHTML => driver.FindElement(By.XPath("//span[text()='Operator guidance HTML']/parent::div//i"));

        By HeadingOrganisation = By.XPath("//h2[@class='display-inline-block margin-bottom-10px'][text()='Organisations']");

        public string OrganisationHeader = "Organisations";
        public string Guidance_TooltipText = "Enter HTML text to display on organisation 'Start Stream' page (optional).";
        public string GuidanceHTML = "//span[@id='opGuidance']//p";


        public bool VerifyOrganisationPage()
        {
            return VerifyPageLoaded(HeadingOrganisation, OrganisationHeader);
        }

        public ConfigurableLandingPage1 EditOrganisation(string OrganisationName, string Value)
        {
            Search(OrganisationName);
            ClickEdit();
            ClickOn(Link_OperatorBranding);
            EnterText(Textarea_OperatorGuidance, Value);
            TakeScreenshot();
            ClickSave();
            VerifyOrganisationPage();
            ExtentReportUtil.report.Log(Status.Pass, "Searched -- <b>" + OrganisationName + "</b> and changed value for Operator Guidance as -- <b>" + Value + "</b>.");
            
            return new ConfigurableLandingPage1();
        }

        public ConfigurableLandingPage1 Search(string OrganisationName)
        {
            ClearText(Input_Search);
            EnterText(Input_Search, OrganisationName);
            return new ConfigurableLandingPage1();
        }

        public ConfigurableLandingPage1 ClickEdit()
        {
            ClickOn(Link_Edit);
            return new ConfigurableLandingPage1();
        }

        public ConfigurableLandingPage1 ClickSave()
        {
            ClickOn(Btn_Save);
            return new ConfigurableLandingPage1();
        }

        public ConfigurableLandingPage1 ClearTextFromGuidanceHTML(IWebElement webElement)
        {
            ClearText(Textarea_OperatorGuidance);
            return new ConfigurableLandingPage1();
        }

        public ConfigurableLandingPage1 ClearGudanceHTML(string OrganisationName)
        {
            Search(OrganisationName);
            ClickEdit();
            ClickOn(Link_OperatorBranding);
            ClearTextFromGuidanceHTML(Textarea_OperatorGuidance);
            ClickSave();
            VerifyOrganisationPage();
            return new ConfigurableLandingPage1();
        }

        public void VerifyOperatorGuidance(string TextToVerify)
        {
            try
            {
                if (Header_OperatorGuidance.Text.Contains(TextToVerify))
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Matches with expected Value -- " + TextToVerify);
                }
            } catch(Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occurred in VerifyOperatorGuidance() method " + ex.ToString());
                TakeScreenshot();
            }
        }

        public ConfigurableLandingPage1 VerifyGuidanceToolTip(string OrganisationName, string TextToVerify)
        {
            try
            {
                Search(OrganisationName);
                ClickEdit();
                ClickOn(Link_OperatorBranding);
                ClickOn(HelpIcon_GuidanceHTML);
                string value = ToolTip.Text;
                if (value.Contains(TextToVerify))
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Matches with expected Value -- " + TextToVerify);
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Values do not match. Actual Value --  " + value);
                }
            } catch(Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occurred in VerifyGuidanceToolTip() method " + ex.ToString());
            }
            return new ConfigurableLandingPage1();
        }

        public void ElementNotPresent_GuidanceHTML()
        {
            ElementNotPresent(GuidanceHTML);
        }
    }
}