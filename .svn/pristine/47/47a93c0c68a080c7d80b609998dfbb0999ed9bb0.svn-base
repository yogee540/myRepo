using Microsoft.Extensions.PlatformAbstractions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework
{
    public class GlobalVariables
    {
        public static IWebDriver driver;
        public static string objectAttributes;
        public static object ElementTitle;
        public static string ElementOperated;
        public static string testCaseID;
        /** 
         Report configuration variables         
         */
        public static string projectFolder = PlatformServices.Default.Application.ApplicationBasePath; //AppDomain.CurrentDomain.SetupInformation.ApplicationBase;//"C:/Users/P10481681/source/repos/CapitaFramework/SeleniumFramework";
        public static string projectPath = projectFolder;//System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public static string logReportFolder = null;
        public static string reportFolder = projectPath.Replace(@"\bin\Debug\netcoreapp2.1","") + "Reports\\" + logReportFolder;
        /**
         * Machine Details variables
         * */
        public static string MachineName = System.Environment.MachineName;
        public static string LoggedInUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        /**
         * Configuration Variables
         * */
        public static string EnvConfigurationPath = projectPath + "/EnvConfig.xml";
        public static string Environment;
        public static string Browser;
        public static string URL;
        public static string AdminURL;
        public static string ReportType;
        public static string ScreenshotType;
        public static string VideoRecordingFlag;
        public static string APIURL;
        public static string Scheme;
        public static string AuthKey;
        public static string SessionId;
        public static string Scheme1;
        public static string AuthKey1;
        public static string Scheme2;
        public static string AuthKey2;
        public static string ZipFolderpath;
        //Tiles

        public static string Maptile = "Map";
        public static string IncidentTile = "Incident";
        public static string ResourceTile = "Resource";
        public static string PoliceWorkerTile = "PoliceWorker";
        public static string LogTile = "Log";
        public static string AlarmInstall = "Alarm Installations";
        public static string AlarmReccoTile = "Alarm Letter Recommendations";

        public static string Username;

        //browserstack
        public static string BrowserStack_Server;
        public static string BrowserStack_Browser;
        public static string BrowserStack_BrowserVersion;
        public static string BrowserStack_OS;
        public static string BrowserStack_OSVersion;
        public static string BrowserStack_User;
        public static string BrowserStack_Key;
        public static string BrowserSatck_Local;
        public static string BrowserStack_realmobile;
        public static string BrowserStack_Device;
        public static string BrowserStack_DeviceOrientation;
        public static string BrowserStack_Parallel;
        

    }
}
