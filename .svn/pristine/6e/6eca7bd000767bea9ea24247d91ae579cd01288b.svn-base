using AventStack.ExtentReports;
using OpenQA.Selenium;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.common;
using SeleniumFramework.Pages;
using SeleniumFramework.Pages.Day1;
using SeleniumFramework.Utilities.reportUtil;
using System;
using System.Collections.Generic;

namespace SeleniumFramework.Pages.Day1
{
    class SensitiveClassification1 : UIActions
    {
        Common commonPage = new Common();
        ConfigurableLandingPage1 configLandingPage = new ConfigurableLandingPage1();
        
        //Tabs
        IWebElement Tab_Users => driver.FindElement(By.XPath("//a[@id='users-tab']"));
        IWebElement Tab_Activities => driver.FindElement(By.XPath("//a[@id='activities-tab']"));

        //Add User Objects
        IWebElement NewUser_Username => driver.FindElement(By.XPath("//label[text()='Username']/following-sibling::input"));
        IWebElement NewUser_Password => driver.FindElement(By.XPath("//label[text()='Password']/following-sibling::input"));
        IWebElement NewUser_ConfirmPassword => driver.FindElement(By.XPath("//label[text()='Confirm Password']/following-sibling::input"));
        IWebElement NewUser_FirstName => driver.FindElement(By.XPath("//label[text()='First Name']/following-sibling::input"));
        IWebElement NewUser_Surname => driver.FindElement(By.XPath("//label[text()='Surname']/following-sibling::input"));
        IWebElement NewUser_Email => driver.FindElement(By.XPath("//label[text()='Email']/following-sibling::input"));
        IWebElement Btn_AddUser => driver.FindElement(By.XPath("//button[@id='add-user-button']"));
        IWebElement CheckMediaDownload_Yes => driver.FindElement(By.XPath("//span[@class='x-toggle-container large x-toggle-container-checked']/div[@id='visual-media-download']"));
        IWebElement CheckMediaDownload_No => driver.FindElement(By.XPath("//span[@class='x-toggle-container large']/div[@id='visual-media-download']"));
        IWebElement MediaDownload => driver.FindElement(By.XPath("//div[@id='visual-media-download']"));
        IWebElement Roles_Checkbox => driver.FindElement(By.XPath("//div[@class='checkbox']"));
        IWebElement Btn_Save => driver.FindElement(By.XPath("//button[text()='Save']"));
        IWebElement Btn_Cancel => driver.FindElement(By.XPath("//a[@id='cancel']"));

        //No Records found
        IWebElement NoRecordsFound => driver.FindElement(By.XPath("//td[text()='No matching records found']"));

        //Activities Objects
        IWebElement Link_ViewSession => driver.FindElement(By.XPath("//table[@id='activities']//a[text()='1']"));
        IWebElement Toggle_ClassifySession => driver.FindElement(By.XPath("//div[@id='visual-sensitive']"));
        IWebElement Input_ClassifyReason => driver.FindElement(By.XPath("//div[@class='panel-body center-text']/textarea"));
        IWebElement Btn_ClassifySubmit => driver.FindElement(By.XPath("//div[@class='panel-body center-text']/button[text()='SUBMIT']"));
        IWebElement Btn_ClassifyCancel => driver.FindElement(By.XPath("//div[@class='panel-body center-text']/button[text()='CANCEL']"));
        IWebElement ClassifySession_ON => driver.FindElement(By.XPath("//span[@class='x-toggle-container large x-toggle-container-checked']/div[@id='visual-sensitive']"));
        IWebElement ClassifySession_OFF => driver.FindElement(By.XPath("//span[@class='x-toggle-container large']/div[@id='visual-sensitive']"));
        IWebElement SenstiveWarning => driver.FindElement(By.XPath("//span[@title='Sensitive']"));
        IWebElement Btn_ReturnToActivities => driver.FindElement(By.XPath("//button[@id='return-to-activities-list-button']"));
        IWebElement Link_Activity => driver.FindElement(By.XPath("//a[text()='Activity']"));

        //Help Objects
        IWebElement HelpIcon => driver.FindElement(By.XPath("//i[@class='fa fa-question-circle-o menu-toggle']"));
        IWebElement UserGuide_Tab => driver.FindElement(By.XPath("//a[text()='User Guide']"));
        IWebElement HelpQues_Classify_Declassify => driver.FindElement(By.XPath("//a[@class='faq-link'][text()='10. How do I classify/de-classify a session as sensitive?']"));
        IWebElement Btn_CloseHelp => driver.FindElement(By.XPath("//i[@class='fa fa-times menu-close']"));

        string SensitiveWarning_XPath  = "//span[@title='Sensitive']";
        By Heading_Users = By.XPath("//h2[contains(text(),'Users')]");
        By SupportOp_Standalone_ErrorMsg = By.XPath("//p[text()='The Support Operator role can only be created as a standalone role.']");
       
        
        public SensitiveClassification1 ClickUsersTab()
        {
            ClickOn(Tab_Users);
            VerifyUsersPageLoaded();
            return new SensitiveClassification1();
        }

        public bool VerifyUsersPageLoaded()
        {
            return VerifyPageLoaded(Heading_Users, "User Page");
        }

        public SensitiveClassification1 AddSupervisotAndOperatorUser(string Password, string Email)
        {
            string timeStamp = commonPage.GetTimeStamp();
            Username = "user_" + timeStamp;

            ClickUsersTab();
            ClickOn(Btn_AddUser);
            EnterText(NewUser_Username, Username);
            EnterText(NewUser_Password, Password);
            EnterText(NewUser_ConfirmPassword, Password);
            EnterText(NewUser_FirstName, Username);
            EnterText(NewUser_Surname, timeStamp);
            EnterText(NewUser_Email, Email);
            Wait(2);
            SelectRoleByIndex("1");
            SelectRoleByIndex("2");
            Wait(1);
            ClickOn(Btn_Save);
            Wait(5);
            VerifyUserInTheUserList("RecordPresent");

            return new SensitiveClassification1();
        }

        public SensitiveClassification1 AddSupportOperatorUser(string Password, string Email)
        {
            string timeStamp = commonPage.GetTimeStamp();
            Username = "user_" + timeStamp;

            ClickUsersTab();
            ClickOn(Btn_AddUser);
            EnterText(NewUser_Username, Username);
            EnterText(NewUser_Password, Password);
            EnterText(NewUser_ConfirmPassword, Password);
            EnterText(NewUser_FirstName, Username);
            EnterText(NewUser_Surname, timeStamp);
            EnterText(NewUser_Email, Email);
            Wait(2);
            SelectRoleByIndex("4");  
            Wait(1);
            ClickOn(Btn_Save);
            Wait(5);
            VerifyUserInTheUserList("RecordPresent");

            return new SensitiveClassification1();
        }

        public void VerifyUserInTheUserList(string VerifyRecord)
        {
            string UserNameInTable = "//table[@id='userTable']//td[@title='" + Username + "']";
            configLandingPage.Search(Username);
            try
            {
                switch (VerifyRecord)
                {
                    case "NoRecords": 
                            if (NoRecordsFound.Displayed)
                            {
                                ExtentReportUtil.report.Log(Status.Pass, "No Records found for -- " + Username);
                            }
                        break;

                    case "RecordPresent":
                            if (driver.FindElement(By.XPath(UserNameInTable)).Displayed)
                            {
                                ExtentReportUtil.report.Log(Status.Pass, "Searched -- <b>" + Username + "</b> is displayed in the User Table");
                            }
                        break;
                    default:
                        break;
                }
            }catch(Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occurred in VerifyUserInTheUserList() method -- " + ex.ToString());
            }
        }

        public void SelectRoleByIndex(string Index)
        {
            string Role = "(//div[@class='checkbox'])[" + Index + "]";
            ClickOn(driver.FindElement(By.XPath(Role)));      
        }

        public void VerifySupportOpIsStandalone()
        {
            configLandingPage.ClickEdit();
            SelectRoleByIndex("5");
            IsElementDisplayed(SupportOp_Standalone_ErrorMsg);
            ClickOn(Btn_Cancel);
            driver.SwitchTo().Alert().Accept();
            Wait(1);
            driver.SwitchTo().Alert().Accept();
       }

        public void VerifyUserInTheUserList_ByName(string UserToVerify, string VerifyRecord)
        {
            string UserNameInTable = "//table[@id='userTable']//td[@title='" + UserToVerify + "']";
            configLandingPage.Search(UserToVerify);
            try
            {
                switch (VerifyRecord)
                {
                    case "NoRecords":
                        if (NoRecordsFound.Displayed)
                        {
                            ExtentReportUtil.report.Log(Status.Pass, "No Records found for -- " + Username);
                        }
                        break;

                    case "RecordPresent":
                        if (driver.FindElement(By.XPath(UserNameInTable)).Displayed)
                        {
                            ExtentReportUtil.report.Log(Status.Pass, "Searched -- <b>" + Username + "</b> is displayed in the User Table");
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occurred in VerifyUserInTheUserList() method -- " + ex.ToString());
            }
          
        }

        public void ClassiySession(string ClassifySession, string Resaon)
        {
            ClickOn(Tab_Activities);
            WaitForObject(Link_ViewSession, 30);
            ClickOn(Link_ViewSession);
            Wait(2);
            ClickOn(Toggle_ClassifySession);
            Wait(1);
            EnterText(Input_ClassifyReason, Resaon);
            ClickOn(Btn_ClassifySubmit);
            VerifySessionClassified(ClassifySession);
            ClickReturnToActivities();
            VerifySensitiveWarning(ClassifySession);
        }
        public void VerifySessionClassified(string SessionClassified)
        {
            try
            {
                switch (SessionClassified.ToUpper())
                {
                    case "YES":

                        if (ClassifySession_ON.Displayed)
                        {
                            ExtentReportUtil.report.Log(Status.Pass, "Session Classified.");
                        }
                        break;
                    case "NO":
                        if (ClassifySession_OFF.Displayed)
                        {
                            ExtentReportUtil.report.Log(Status.Pass, "Session Declassified.");
                        }
                        break;
                }
            }catch(Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occurred in VerifySessionClassified() method -- " + ex.ToString());
                TakeScreenshot();
            }
        
        }

        public void ClickReturnToActivities()
        {
            try
            {
                ClickOn(Btn_ReturnToActivities);
                WaitForObject(Link_ViewSession, 30);
                
            }catch(Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occurred in ClickReturnToActivities() method -- " + ex.ToString());
                TakeScreenshot();
            }
        }

        public void VerifySensitiveWarning(string SensitiveWarningDisplayed)
        {
            try
            {
                switch (SensitiveWarningDisplayed.ToUpper())
                {
                    case "YES":
                        if (SenstiveWarning.Displayed)
                        {
                            ExtentReportUtil.report.Log(Status.Pass, "Warning displayed.");
                        }
                        break;
                    case "NO":
                        ElementNotPresent(SensitiveWarning_XPath);
                        ExtentReportUtil.report.Log(Status.Pass, "Warning Not displayed.");
                        break;
                }
            }catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occurred in VerifySensitiveWarning() method -- " + ex.ToString());
            }
          
        }

        public void VerifyHelpTextForClassifyDeclassifySession(string ParaValue)
        {
            try
            {
                IList<IWebElement> Help_Classify = driver.FindElements(By.XPath("//div[@class='panel-collapse collapse in']//p"));                
                foreach (IWebElement el in Help_Classify)
                {
                    string para = el.GetAttribute("innerText");
                    if (para.Contains(ParaValue))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Content displayed.");
                        break;
                    }
                }
            }catch(Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occurred in VerifyHelpTextForClassifyDeclassifySession() method -- " + ex.ToString());
            }
                    
        }

        

        public SensitiveClassification1 ClickUserGuide()
        {
            ClickOn(UserGuide_Tab);
            WaitForObject(HelpQues_Classify_Declassify, 30);
            return new SensitiveClassification1();
        }

        public SensitiveClassification1 ClickHelpQuestion()
        {
            ClickOn(HelpQues_Classify_Declassify);
            return new SensitiveClassification1();
        }

        public SensitiveClassification1 CloseHelp()
        {
            ClickOn(Btn_CloseHelp);
            return new SensitiveClassification1();
        }

        public SensitiveClassification1 ClickActivityLink()
        {
            ClickOn(Link_Activity);
            return new SensitiveClassification1();
        }
    }
}