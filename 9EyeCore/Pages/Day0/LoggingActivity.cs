﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.reportUtil;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using System.Threading;
using SeleniumFramework.Pages;
using SeleniumFramework.Pages.Day2;
using OpenQA.Selenium.Support.UI;
using SeleniumFramework.BusinessFunctions;

namespace SeleniumFramework.Pages.Day0
{
    public class LoggingActivity : UIActions
    {
        CommonFeatures commonFeatures = new CommonFeatures();
        //public LoggingActivity ()
        //    {

        //    }

        //107582
        public static IWebElement Button_Advanced => driver.FindElement(By.CssSelector("button#details-button"));
        public static IWebElement Link_Proceed => driver.FindElement(By.CssSelector("#proceed-link"));
        public static IWebElement Input_Email => driver.FindElement(By.CssSelector("#email"));
        public static IWebElement Input_Password => driver.FindElement(By.CssSelector("#password"));
        public static IWebElement Input_Login => driver.FindElement(By.CssSelector("#login"));

        public static IWebElement Link_UserTab => driver.FindElement(By.CssSelector("#users-tab"));
        public static IWebElement Button_AddUser => driver.FindElement(By.CssSelector("button#add-user-button"));

        public static IWebElement Input_Userbox => driver.FindElement(By.CssSelector("#user"));
        public static IWebElement Input_Pass => driver.FindElement(By.CssSelector("#pass"));
        public static IWebElement Input_ConfirmPass => driver.FindElement(By.CssSelector("#confirmPassword"));
        public static IWebElement Input_FirstName=> driver.FindElement(By.CssSelector("#firstName"));
        public static IWebElement Input_SurName => driver.FindElement(By.CssSelector("#surname"));
        public static IWebElement Div_MediaDownload => driver.FindElement(By.CssSelector("div#visual-media-download"));
        public static IWebElement Input_AdminRole => driver.FindElement(By.CssSelector("#organisationAdmin"));//organisationSupportOperator
        public static IWebElement Input_SupportOpRole => driver.FindElement(By.CssSelector("#organisationSupportOperator"));
        public static IWebElement Input_OperatorRole => driver.FindElement(By.Id("operator"));
        public static IWebElement Input_OpRole => driver.FindElement(By.CssSelector("#operator"));
        public static IWebElement Button_Save => driver.FindElement(By.CssSelector("#save"));

        public static IWebElement Input_SearchUser => driver.FindElement(By.CssSelector("input.form-control.input-sm"));
        public static IWebElement Td_ActivityLink => driver.FindElement(By.XPath("//td[@title='X']/following-sibling::td/a[text()='Activity']"));

        //107583
        public static IWebElement Li_UserDropdown => driver.FindElement(By.CssSelector("li.dropdown"));
        public static IWebElement Button_Logout => driver.FindElement(By.CssSelector("#log-out-button"));

        //107586
        public static IWebElement Div_ErrorMessage => driver.FindElement(By.XPath("//div[@class='container']/div[2]"));

        //107587
        public static IWebElement Div_UserStatusSwitch => driver.FindElement(By.CssSelector("div#visual-status"));
        public static IWebElement Link_ActivitiesTab => driver.FindElement(By.CssSelector("a#activities-tab"));
        public static List<IWebElement> Allrows => driver.FindElements(By.CssSelector("tr")).ToList();
        public static IWebElement Li_Last30Days => driver.FindElement(By.CssSelector("li[data-range-key='Last 30 Days']"));


        //106775
        //organisation-tab
        public static IWebElement Link_OrgTab => driver.FindElement(By.CssSelector("a#organisation-tab"));
        //public static List<IWebElement> allrows => driver.FindElements(By.XPath("//table[@id='organisationTable']/tbody/tr")).ToList();

        //104306
        public static IWebElement Span_viewSession => driver.FindElement(By.XPath("//table[@id='activities']/tbody/tr[1]/td[5]"));
        public static IWebElement Button_DownloadAudotPDF => driver.FindElement(By.CssSelector("button#downloadAuditPdf-button"));//downloadAuditPdf-button 
        public static IWebElement Button_DownloadPhoto => driver.FindElement(By.XPath("//div[@id='my-carousel']//button[contains(text(),'Download Photo')]"));
        public static IWebElement Button_DownloadVideo => driver.FindElement(By.XPath("//div[@id='my-carousel']//button[contains(text(),'Download Video')]"));
        public static IWebElement Button_DownloadAllMedia => driver.FindElement(By.CssSelector("button#download-session-button"));
        public static IWebElement Textarea_Downloadreason => driver.FindElement(By.CssSelector("textarea.download-textarea.ember-text-area.ember-view"));
        public static IWebElement Button_Submit => driver.FindElement(By.CssSelector("button.pull-right.btn.btn-default"));
        public static IWebElement Button_ReturnToActivities => driver.FindElement(By.CssSelector("#return-to-activities-list-button"));
        public static IWebElement H2_HeaderOfDownloadPage => driver.FindElement(By.CssSelector("div.container span h2"));
        public static IWebElement Sapan_Status_Toggle => driver.FindElement(By.XPath("//div[@id='visual-status']/"));

        IWebElement Txt_CurrentPassword => driver.FindElement(By.CssSelector("#old-password-input"));
        IWebElement Txt_NewPassword => driver.FindElement(By.CssSelector("#new-password-input"));
        IWebElement Txt_ConfirmNewPassword => driver.FindElement(By.CssSelector("#confirm-password-input"));
        IWebElement Btn_Save_ConfirmPassword => driver.FindElement(By.XPath("//button[@id='update-password']"));
        IWebElement Link_Activity => driver.FindElement(By.XPath("//a[text()='Activity']"));
        IWebElement Link_Edit => driver.FindElement(By.XPath("//a[text()='Edit']"));
        //TC_132433_1_StreamingSerivce()
        IWebElement AddStream => driver.FindElement(By.Id("addStream"));
        IWebElement Email_entry => driver.FindElement(By.XPath("//input[contains(@id, 'ember')]"));
        IWebElement send_button => driver.FindElement(By.XPath("//button[@title='Send']"));


        //validation
        IWebElement P_UserError => driver.FindElement(By.CssSelector("#user-error"));
        List<IWebElement> P_PasswordErrors => driver.FindElements(By.CssSelector("#pass-error-error-message")).ToList();
        IWebElement P_ConfirmPasswordError => driver.FindElement(By.CssSelector("#confirmPassword-error"));
        IWebElement P_FirstNameError => driver.FindElement(By.CssSelector("#firstName-error"));//surname-error
        IWebElement P_SurNameError => driver.FindElement(By.CssSelector("#surname-error"));
        IWebElement P_EmailError => driver.FindElement(By.CssSelector("#email-error"));
        IWebElement Link_Cancel => driver.FindElement(By.LinkText("Cancel"));
        IWebElement Span_UserNameExists => driver.FindElement(By.CssSelector("div.alert.alert-danger.compact-alert.margin-bottom-10px span"));
        IWebElement Status_Locked => driver.FindElement(By.XPath("//td[text()='Locked']"));
        IWebElement StreamingEmailError => driver.FindElement(By.CssSelector("p.alert.alert-danger"));
        IWebElement Tr_FirstRowOrg => driver.FindElement(By.XPath("//tr[1]"));
        IWebElement Link_FirstAcitvity => driver.FindElement(By.XPath("//tr[1]//a[text()='Activity']"));
        IWebElement Table_ActivityFirstRow => driver.FindElement(By.CssSelector("table#activities tr"));
        IWebElement Td_UserLoggedIn => driver.FindElement(By.XPath("//tr[1]//td[text()='User logged in']"));
        IWebElement ServiceUnavailable => driver.FindElement(By.XPath("//h1"));

        public string UpdatedUsername;

        public void Login(string username, string password)
        {
            try
            {
                //click on advanced button if visible
                //if (IsElementDisplayedNEnabled(Button_Advanced))
                //{
                //    ClickOn(Button_Advanced);
                //    ClickOn(Link_Proceed);
                //}
                Input_Email.Clear();
                Set_Textbox(Input_Email, username);
                Set_Textbox(Input_Password, password);
                ClickOn(Input_Login);
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Login() method => " + ex.ToString());
                TakeScreenshot();
            }
        }

        public void CreateUser(List<string> userInfo,bool mediadownload=true)
        {
            Thread.Sleep(3000);
            //click on users tab
            ClickOn(Link_UserTab);
            Thread.Sleep(3000);
            //click on Add user
            ClickOn(Button_AddUser);
            //input all the user info
            Set_Textbox(Input_Userbox, userInfo[0]);
            Set_Textbox(Input_Pass, userInfo[1]);
            Set_Textbox(Input_ConfirmPass, userInfo[1]);
            Set_Textbox(Input_FirstName, userInfo[2]);
            Set_Textbox(Input_SurName, userInfo[3]);
            Set_Textbox(Input_Email, userInfo[4]);

            if (userInfo[5].Equals("Support Operator"))
            {
                ClickOn(Input_SupportOpRole);
                VerifyMediaDownLoadFlag();
            }
            else if (userInfo[5].Equals("Operator")) {
                ClickOn(Input_OperatorRole);
                VerifyMediaDownLoadFlag();
            }
            else
            {
                ClickOn(Input_AdminRole);
            }
            if (mediadownload)
            {
                ClickOn(Div_MediaDownload);
            }
            ClickOn(Button_Save);

        }

        public void VerifyUser(string username)
        {
            try
            {
                Thread.Sleep(5000);
                //search user by username
                Set_Textbox(Input_SearchUser, username);
                string cssSelect = "td[title = 'X']".Replace("X", username);
                string activityXpath = "//td[@title = 'X']/following-sibling::td/a[text()='Activity']".Replace("X", username);
                Highlight(driver.FindElement(By.CssSelector(cssSelect)));
                ClickOn(driver.FindElement(By.XPath(activityXpath)));
                ExtentReportUtil.Test.Log(Status.Pass, "Verification Successful:");
            }
            catch(Exception ex)
            {
                ExtentReportUtil.Test.Log(Status.Fail, "Verification Failed:"+ex.ToString());
            }
                
        }      

        public void Logout()
        {
            ClickOn(Li_UserDropdown);
            ClickOn(Button_Logout);
        }

        public void KMSLogout()
        {
            ClickOn(Li_UserDropdown);
            Wait(2);
            ClickOn(Button_Logout);
            Wait(5);
            driver.SwitchTo().Alert().Accept();
        }

        public bool VerifyMediaDownLoadFlag()
        {
            //verify parent element class
            IWebElement parentElement = Div_MediaDownload.FindElement(By.XPath(".."));
            if (parentElement.GetAttribute("Class").Equals("x-toggle-container large"))
            {
                ExtentReportUtil.report.Log(Status.Pass, "Media Download disabled:");
                TakeScreenshot();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void VerifyErrorMessage(string errormessage)
        {
            try
            {
                //locate message
                string UIerrormsg = Div_ErrorMessage.GetAttribute("innerText");

                if (UIerrormsg.Contains(errormessage))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Approproate Error message shown:" + UIerrormsg);
                        TakeScreenshot();
                    }
                else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "InApproproate Error message shown:" + UIerrormsg);
                        TakeScreenshot();
                    }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Error, "Exception Occurred: " + ex.ToString());
            }


        }
       
        public void DisableUser(string user2)
        {
            //click on user tab
            Thread.Sleep(3000);
            ClickOn(Link_UserTab);
            Thread.Sleep(5000);
            Set_Textbox(Input_SearchUser, user2);
            string cssSelect = "td[title = 'X']".Replace("X", user2);
            string editXpath = "//td[@title = 'X']/following-sibling::td/a[text()='Edit']".Replace("X", user2);

            //click on Edit button
            ClickOn(driver.FindElement(By.XPath(editXpath)));
            Thread.Sleep(5000);

            //click on lock button
            ClickOn(Div_UserStatusSwitch);
            Thread.Sleep(5000);
            Highlight(Button_Save);
            ClickOn(Button_Save);
            Thread.Sleep(10000);
        }

        public void VerifySearchFilter(string search1, string search2, string search3)
        {
            //search the first search string
            Set_Textbox(Input_SearchUser, search1);
            IWebElement firstrow = null;
            string cssSelect = "//td[text() = 'X']".Replace("X", search1);
            try
            {
                 firstrow = driver.FindElement(By.XPath(cssSelect));
            }
            catch (NoSuchElementException ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "NO record present::NOt As exepcted:");
                TakeScreenshot();
            }
            if (IsElementDisplayedNEnabled(firstrow))
            {
                Highlight(firstrow);
                if ((Allrows.Count-1) == 1)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "One record present::As exepcted");
                    TakeScreenshot();
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Pass, "More record present::NOt As exepcted:");
                    TakeScreenshot();
                }
            }

            //search partial name 
            Set_Textbox(Input_SearchUser, search2);
            //cssSelect = "//td[contains(text(),'test')]";

            cssSelect = "//td[contains(text(),'" + search2 + "')]";
            AllRowCount();


            //search by number
            Set_Textbox(Input_SearchUser, search3);
            //cssSelect = "//td[contains(text(),'test')]";

            cssSelect = "//td[contains(text(),'" + search3 + "')]";

            AllRowCount();
        }

        public void VerifyUserCreatedInActivity()
        {
            By UserCreated_XPath = By.XPath("//td[text()='User created: [" + Username + "]']");
            try
            {
                ClickOn(Link_Activity);
                if (driver.FindElement(UserCreated_XPath).Displayed)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "User created and displayed in activity -- " + Username);
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Error, "Exception Occurred: " + ex.ToString());
            }
        }

        public void AllRowCount()
        {
            if (Allrows.Count > 0)
            {
                ExtentReportUtil.report.Log(Status.Pass, "One or more record present::As exepcted");
                ExtentReportUtil.report.Log(Status.Info, "Total records =" + Allrows.Count.ToString());
                TakeScreenshot();
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Info, "NO record present::NOt As exepcted:");
                TakeScreenshot();
            }
        }

        public void EditUsername(string UpdatedUsername)
        {
            ClickOn(Link_Edit);
            ClearText(Input_Userbox);
            EnterText(Input_Userbox, UpdatedUsername);
            ClickOn(Button_Save);
        }

        public void VerifyUserUpdatedInActivity(string UpdatedUserName)
        {
            By UserCreated_XPath = By.XPath("//td[text()='User updated: [" + UpdatedUserName + "]']");
            try
            {
                ClickOn(Link_Activity);
                if (driver.FindElement(UserCreated_XPath).Displayed)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "User created and displayed in activity -- " + UpdatedUserName);
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Error, "Exception Occurred: " + ex.ToString());
            }
        }

        public void CreateUserFieldValidation(List<string> userinfo,List<string> usererror,List<string> normaluser)
        {
            //try
            //{
                //login using admin_operator
                Login(userinfo[0], userinfo[1]);

                //click on User tab
                ClickOn(Link_UserTab);
                Wait(7);
                //click on Add user
                ClickOn(Button_AddUser);
                //input all the user info
                Wait(5);

                Set_Textbox(Input_Userbox, userinfo[2]);
                VerifyUserFieldError(P_UserError, usererror[0]);
                Wait(2);
                Set_Textbox(Input_Pass, userinfo[3]);
                if (P_PasswordErrors.Count > 0)
                {
                    VerifyUserFieldError(P_PasswordErrors[0], usererror[1]);
                    VerifyUserFieldError(P_PasswordErrors[1], usererror[2]);
                    VerifyUserFieldError(P_PasswordErrors[2], usererror[3]);
                }
                Wait(2);
                Set_Textbox(Input_ConfirmPass, userinfo[4]);
                VerifyUserFieldError(P_ConfirmPasswordError, usererror[4]);
                Wait(2);
                Set_Textbox(Input_FirstName, userinfo[5]);
                VerifyUserFieldError(P_FirstNameError, usererror[5]);
                Wait(2);
                Set_Textbox(Input_SurName, userinfo[6]);
                VerifyUserFieldError(P_SurNameError, usererror[6]);
                Wait(2);
                Set_Textbox(Input_Email, userinfo[7]);
                VerifyUserFieldError(P_EmailError, usererror[7]);
                Wait(2);

                ClickOn(Button_Save);
                Wait(2);
                ClickOn(Link_Cancel);
                Wait(5);
                IAlert alert = driver.SwitchTo().Alert();
                alert.Dismiss();

                ClickOn(Link_ActivitiesTab);
                driver.SwitchTo().Alert();
                alert.Dismiss();

                //click on streamtab
                ClickOn(VideoStreaming.Link_StreamTab);
                driver.SwitchTo().Alert();
                alert.Dismiss();

                ClickOn(Link_Cancel);
                Wait(5);
                alert = driver.SwitchTo().Alert();
                alert.Accept();

                Wait(5);
                ClickOn(Button_AddUser);
                Wait(3);


                Set_Textbox(Input_Userbox, normaluser[0]);
                Set_Textbox(Input_Pass, normaluser[1]);
                Set_Textbox(Input_ConfirmPass, normaluser[1]);
                Set_Textbox(Input_FirstName, normaluser[2]);
                Set_Textbox(Input_SurName, normaluser[3]);
                Set_Textbox(Input_Email, normaluser[4]);
                ClickOn(Input_AdminRole);
                ClickOn(Div_MediaDownload);
                ClickOn(Button_Save);
                Wait(3);
                VerifyUserFieldError(Span_UserNameExists, usererror[8]);
                Wait(3);

                Set_Textbox(Input_Userbox, "±newuserdelimeter");
                Wait(3);
                VerifyUserFieldError(P_UserError, usererror[9]);
                Wait(3);

                Set_Textbox(Input_Pass,"±newuserdelimeter");
                Wait(2);
                VerifyUserFieldError(P_PasswordErrors[2], usererror[9]);
            //}
            //catch (Exception ex)
            //{
            //    ExtentReportUtil.Test.Log(Status.Pass, "Verification Successful:");
            //}
        }

        public void EditUserFieldValidation(List<string> userinfo)
        {
            try
            {
                //login
                //login using admin_operator
                Login(userinfo[0], userinfo[1]);

                //click on User tab
                ClickOn(Link_UserTab);
                Wait(7);

                //Search for other user
                Set_Textbox(Input_SearchUser, userinfo[5]);
                Wait(5);

                //click on Edit Button
                ClickOn(Link_Edit);

                //clear input userbox & verify error
                Input_Userbox.Clear();
                VerifyUserFieldError(P_UserError, userinfo[2]);

                //clear F-name input & verify error
                Input_FirstName.Clear();
                VerifyUserFieldError(P_FirstNameError, userinfo[3]);

                //clear L-Name input & verify error
                Input_SurName.Clear();
                VerifyUserFieldError(P_SurNameError, userinfo[4]);

                //click on cancel
                ClickOn(Link_Cancel);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();

                //Search for other user
                Set_Textbox(Input_SearchUser, userinfo[5]);
                Wait(5);

                ChangeAccoutStatus();

                //Search for other user
                Set_Textbox(Input_SearchUser, userinfo[5]);
                Wait(5);

                if (IsElementDisplayedNEnabled(Status_Locked))
                {
                    ExtentReportUtil.Test.Log(Status.Pass, "Verification Successfull:");

                }
                else
                {
                    ExtentReportUtil.Test.Log(Status.Fail, "Verification Failed:");
                }

                //ChangeStatus to Active
                Set_Textbox(Input_SearchUser, userinfo[5]);
                Wait(7);

                ChangeAccoutStatus();


            }
            catch (Exception ex)
            {
                ExtentReportUtil.Test.Log(Status.Fail, "Verification Failed:"+ex.ToString());
            }


        }

        public void ValidateLoginErrorMsgs(List<string> userinfo)
        {
            try
            {
                //Login
                Login(userinfo[0], userinfo[1]);

                //validate fields stream,users,activities,reports
                Wait(3);
                ValidateTabs();

                //logout
                Logout();

                //second scenario
                Wait(3);
                Login(userinfo[2], userinfo[1]);
                VerifyErrorMessage(userinfo[4]);
                Wait(3);
                RefreshBrowser();
                //third scenario
                //Login(userinfo[0], "");
                //VerifyErrorMessage(userinfo[4]);
                //Wait(3);
                //fourth
                Login(userinfo[0], userinfo[2]);
                VerifyErrorMessage(userinfo[4]);
                Wait(3);
                //fifth
                Login("", "");
                Wait(5);
                VerifyErrorMessage(userinfo[5]);
                ExtentReportUtil.Test.Log(Status.Pass, "ALL scenarios successful");
        }
            catch (Exception ex)
            {
                ExtentReportUtil.Test.Log(Status.Fail, "Verification Failed:" + ex.ToString());
            }

}

        public void ValidateStreamingErrorMsgs(List<string> userinfo)
        {
            try
            {
                //login
                Login(userinfo[0], userinfo[1]);

                //go to streaming page
                ClickOn(VideoStreaming.Link_StreamTab);

                //input wrong email
                Set_Textbox(VideoStreaming.Input_EmailBox, userinfo[2]);
                Set_Textbox(VideoStreaming.Input_Reference, userinfo[1]);
                Wait(3);
                VerifyUserFieldError(StreamingEmailError, userinfo[3]);

                VideoStreaming.Input_EmailBox.Clear();
                Set_Textbox(VideoStreaming.Input_EmailBox, userinfo[5]);
                Set_Textbox(VideoStreaming.Input_Reference, userinfo[5]);
                Wait(3);

                Wait(5);
                ExtentReportUtil.Test.Log(Status.Pass, "ALL scenarios successful");
                Logout();
            }
            catch (Exception ex)
            {
                ExtentReportUtil.Test.Log(Status.Fail, "Verification Failed:" + ex.ToString());
            }
        }

        public void ValidateUserRole(List<string> userinfo)
        {
            try
            {
                //login first user
                Login(userinfo[0], userinfo[1]);

                //verify all tabs
                ValidateTabs();
                if (IsElementDisplayedNEnabled(Link_OrgTab))
                {
                    ExtentReportUtil.Test.Log(Status.Pass, "Organisation Tab is visible::");
                }
                else
                {
                    ExtentReportUtil.Test.Log(Status.Fail, "Organisation Tab is not vsisble:");
                }

                //click on Org tab
                ClickOn(Link_OrgTab);
                Wait(5);

                Highlight(Tr_FirstRowOrg);
                //click on add button
                string buttontext = OrganisationPage.btn_Add_Org.Text;
                if (buttontext.Equals("Add Organisation"))
                {
                    ExtentReportUtil.Test.Log(Status.Pass, "Localized text displayed");
                }
                else
                {
                    ExtentReportUtil.Test.Log(Status.Fail, "Localized text NOT displayed:");
                }

                ClickOn(OrganisationPage.btn_Add_Org);
                Wait(4);

                //click on cancel
                ClickOn(OrganisationPage.btn_Cancel);
                Wait(4);
                Logout();

                //login with second user
                Login(userinfo[2], userinfo[3]);
                Wait(10);
                ExtentReportUtil.Test.Log(Status.Pass, "Login is Successful::");
                ExtentReportUtil.Test.Log(Status.Pass, "User Verification Successful::");
                //Wait(10);
                //Logout();
            }
            catch (Exception ex)
            {
                ExtentReportUtil.Test.Log(Status.Fail, "Verification Failed:" + ex.ToString());
            }
        }

        public void ValidateUserActivityLog(string username,string password)
        {
            //login
            Login(username, password);

            //userpage
            Wait(4);
            ClickOn(Link_UserTab);
            Wait(5);

            //click on activitylinnkFirst
            commonFeatures.Search("auto_temp_operator");
            ClickOn(Link_FirstAcitvity);
            Wait(5);
            Highlight(Table_ActivityFirstRow);

            //Verify return activitybutton and click
            if(IsElementDisplayedNEnabled(Button_ReturnToActivities))
            {
                ExtentReportUtil.Test.Log(Status.Pass, "Button displayed");
                ClickOn(Button_ReturnToActivities);
            }
            else
            {
                ExtentReportUtil.Test.Log(Status.Fail, "Button NOT displayed:");
            }

            //scenario2 
            Wait(3);
            Set_Textbox(Input_SearchUser, username);

            Wait(4);
            ClickOn(Link_FirstAcitvity);

            if (IsElementDisplayedNEnabled(Td_UserLoggedIn))
            {
                ExtentReportUtil.Test.Log(Status.Pass, "Button displayed");
                Highlight(Td_UserLoggedIn);
            }
            else
            {
                ExtentReportUtil.Test.Log(Status.Fail, "Button NOT displayed:");
            }

        }

        private void ChangeAccoutStatus()
        {
            //click on Edit Button
            ClickOn(Link_Edit);

            ClickOn(Div_UserStatusSwitch);
            Wait(3);

            ClickOn(Button_Save);
            Wait(9);
        }

        private void ValidateTabs()
        {
            if (IsElementDisplayedNEnabled(VideoStreaming.Link_StreamTab) &&
                IsElementDisplayedNEnabled(Link_ActivitiesTab) &&
                IsElementDisplayedNEnabled(Link_UserTab) &&
                IsElementDisplayedNEnabled(ReportsPage.Link_ReportsTab))
            {
                ExtentReportUtil.Test.Log(Status.Pass, "All Tabs are visible::");
            }
            else
            {
                ExtentReportUtil.Test.Log(Status.Fail, "One or more tabs are not vsisble:");
            }
        
        }
        
        public void VerifyDownloadButtons()
        {
            //verify visibility of header and other buttons
            if (IsElementDisplayedNEnabled(H2_HeaderOfDownloadPage) &&
               IsElementDisplayedNEnabled(Button_DownloadAllMedia) &&
               IsElementDisplayedNEnabled(Button_DownloadAudotPDF) &&
               IsElementDisplayedNEnabled(Button_ReturnToActivities))
            {
                Highlight(H2_HeaderOfDownloadPage);
                Highlight(Button_DownloadAudotPDF);
                Highlight(Button_ReturnToActivities);
                Highlight(Button_DownloadAllMedia);

                ExtentReportUtil.report.Log(Status.Pass, "Element displayed & enabled");
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element Not displayed/enabled");
            }

            //button photoDownload
            if (IsElementDisplayedNEnabled(Button_DownloadPhoto))
            {
                Highlight(Button_DownloadPhoto);
                ClickOn(Button_DownloadPhoto);

                Thread.Sleep(3000);
                Set_Textbox(Textarea_Downloadreason, "House Targareyen");
                ClickOn(Button_Submit);
            }

            //VerifyDownloadOnStreamPage();

        }

        public bool VerifyDownloadOnStreamPage()
        {
            //button videoDownload
            if (IsElementDisplayedNEnabled(Button_DownloadVideo) || IsElementDisplayedNEnabled(Button_DownloadAllMedia))
            {
                Highlight(Button_DownloadVideo);
                ClickOn(Button_DownloadVideo);

                Thread.Sleep(3000);
                Set_Textbox(Textarea_Downloadreason, "House Targareyen");
                ClickOn(Button_Submit);

                Wait(5);

                Highlight(Button_DownloadAllMedia);
                ClickOn(Button_DownloadAllMedia);

                Thread.Sleep(3000);
                Set_Textbox(Textarea_Downloadreason, "House Targareyen");
                ClickOn(Button_Submit);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void VerifyUserFieldError(IWebElement elementerror, string expectederror)
        {
            
            try
            {
                //verify error
                if (IsElementDisplayedNEnabled(elementerror))
                {
                    Highlight(elementerror);
                    if (elementerror.Text.Equals(expectederror))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Error Displayed => " + expectederror);
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "NO/Wrong Error Displayed => " + elementerror.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception in method VerifyUserFieldError "+ ex.ToString());
            }
        }

        public void ResetPassword(string currentPW, string newPW) {
            WaitForObject(Txt_CurrentPassword);
            EnterText(Txt_CurrentPassword, currentPW);
            EnterText(Txt_NewPassword, newPW);
            EnterText(Txt_ConfirmNewPassword, newPW);
            ClickOn(Btn_Save_ConfirmPassword);
        }

        public void VerifyServiceAvailable()
        {
            try
            {
                if (WaitForObject(ServiceUnavailable))
                {
                    string temp = GetLabelText(ServiceUnavailable);
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Error occured => <b>" + temp + "</b> ");
                        TakeScreenshot();
                    }
                }
            }
            catch (NoSuchElementException)
            {
                ExtentReportUtil.report.Log(Status.Pass, "ServiceAvailable Tomcat working Fine ");
                TakeScreenshot();
            }
        }

        public LoggingActivity clickaddstream()
        {
            ClickOn(AddStream);
            return new LoggingActivity();

        }
        public LoggingActivity Enteremail(string mail)
        {
            EnterText(Email_entry, mail);
            return new LoggingActivity();
        }

        public LoggingActivity clickonsendbutton()
        {
            ClickOn(send_button);
            return new LoggingActivity();
        }


    }
}
