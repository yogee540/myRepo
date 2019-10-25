using AventStack.ExtentReports;
using OpenQA.Selenium;
using SeleniumFramework.UIOperations;
using SeleniumFramework.Utilities.reportUtil;
using System;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SeleniumFramework.Pages.Day0;
using System.Threading;
using System.Collections.Generic;

namespace SeleniumFramework.Pages
{
    class LoginPage : UIActions
    {
        

        //Login Page Objects
        public static IWebElement LogIn_Page => driver.FindElement(By.XPath("//h3[text()='Log in']"));
        public static IWebElement UserName => driver.FindElement(By.XPath("//input[@id='email']"));
        public static IWebElement Password => driver.FindElement(By.XPath("//input[@id='password']"));
        public static IWebElement Btn_Login => driver.FindElement(By.XPath("//button[@id='login']"));
        public static IWebElement Btn_Save => driver.FindElement(By.XPath("//button[@id='save']"));
        

        //Logout Objects
        public static IWebElement Btn_LogOut => driver.FindElement(By.CssSelector("#log-out-button"));
        public static IWebElement LogOut_Arrow => driver.FindElement(By.XPath("(//div[@class='pull-right']//a)[1]"));

        //Change Password Objects
        public static IWebElement Input_OldPassword => driver.FindElement(By.XPath("//input[@id='old-password-input']"));
        public static IWebElement Input_NewPassword => driver.FindElement(By.XPath("//input[@id='new-password-input']"));
        public static IWebElement Input_ResetPassword => driver.FindElement(By.XPath("//input[@id='pass']"));
        public static IWebElement Input_ConfirmResetPassword => driver.FindElement(By.XPath("//input[@id='confirmPassword']"));

        public static IWebElement Input_ConfirmPassword => driver.FindElement(By.XPath("//input[@id='confirm-password-input']"));
        public static IWebElement Btn_Save_ConfirmPassword => driver.FindElement(By.XPath("//button[@id='update-password']"));

        //Tabs
        public static IWebElement Tab_Users => driver.FindElement(By.XPath("//a[@id='users-tab']"));
        public static IWebElement Tab_Activities => driver.FindElement(By.XPath("//a[@id='activities-tab']"));
        public static IWebElement Tab_Organisations => driver.FindElement(By.CssSelector("#organisation-tab"));
        public static IWebElement Tab_Stream => driver.FindElement(By.CssSelector("#stream-tab"));
        //Verify lock & unlock if locked- User Page
        IWebElement Input_Search_USer => driver.FindElement(By.XPath("//input[@type='search']"));
        IWebElement Td_locked_User => driver.FindElement(By.XPath(""));
        //td[@title='madhu-temp-op']
        public static IWebElement Div_Status_Toggle => driver.FindElement(By.XPath("//div[@id='visual-status']"));
        public static IWebElement Button_Save => driver.FindElement(By.CssSelector("button#save"));

        //Unsupporrted Browser Objects
        public static IWebElement BrowserNotSupportedMessageFX => driver.FindElement(By.XPath("//h2[text()='Unsupported Browser']"));
        public static IWebElement BrowserNotSupportedMessageIE => driver.FindElement(By.CssSelector("div.body1 h2"));

       
        

        public static string LoginPageHeader = "Sign in";

        public bool VerifyLoginPage()
        {
            By Login_PageHeader;
            if (GlobalVariables.URL.Contains("auto"))
            {
                Login_PageHeader = By.XPath("//h3[text()='Log in']");
            }
            else
            {
                 Login_PageHeader = By.XPath("//h3[text()='Sign in']");
            }
            return VerifyPageLoaded(Login_PageHeader, LoginPageHeader);
        }

        public LoginPage LoginToApplication(string username, string password)
        {
            try
            {
                //By Homepage_Header = By.XPath("//h4[text()='QA Capita Admin Organisation']");
               // By Homepage_Header = By.XPath("//h4[text()='QA Capita Admin Organisation']");
                VerifyLoginPage();
                Wait(2);
                EnterText(UserName, username);
                EnterText(Password, password);
                ClickOn(Btn_Login);
                Wait(2);
               // VerifyPageLoaded(Homepage_Header, "QA Capita Admin Organisation");
                
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception Occured in LoginToApplication() Method " + ex.ToString());
                TakeScreenshot();
            }
            return new LoginPage();
        }

     
        public void LogOffFromApplication()
        {
            try
            {
                ClickOn(LogOut_Arrow);
                ClickOn(Btn_LogOut);
                Wait(2);
                VerifyLoginPage();
                ExtentReportUtil.report.Log(Status.Pass, "Logout successful.");
            }
            catch(Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured => <b>" + ex.ToString());
            }            
        }

        public LoginPage ChangePassword(string OldPassword, string NewPassword)
        {
            try
            {
                EnterText(Input_OldPassword, OldPassword);
                EnterText(Input_NewPassword, NewPassword);
                EnterText(Input_ConfirmPassword, NewPassword);
                Wait(2);
                ClickOn(Btn_Save_ConfirmPassword);
                WaitForObject(Tab_Users, 30);               
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception Occured in LoginToApplication() Method " + ex.ToString());
                TakeScreenshot();
            }
            return new LoginPage();
        }

        public LoginPage ResetPassword(string NewPassword)
        {
            try
            {
                EnterText(Input_ResetPassword, NewPassword);
                EnterText(Input_ConfirmResetPassword, NewPassword);
                Wait(2);
                ClickOn(Btn_Save);
                Wait(5);
                WaitForObject(Tab_Users, 30);
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception Occured in LoginToApplication() Method " + ex.ToString());
                TakeScreenshot();
            }
            return new LoginPage();
        }

        public void VerifyLoginUsingMultipleUser(List<string> userlist, string password)
        {
            try
            {
                for (int i = 0; i < userlist.Count; i++)
                {
                    LoginToApplication(userlist[i], password);
                    LogOffFromApplication();
                    ExtentReportUtil.report.Log(Status.Pass, "User1 logged in Successfully."+ userlist[i]);
                }
                ExtentReportUtil.report.Log(Status.Pass, "ALL user  logged in Successfully.");
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Verify9EyeApplicationInChrome() method " + ex.ToString());
                TakeScreenshot();

            }

        }
     
    }
}