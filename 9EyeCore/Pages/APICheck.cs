﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Web;
using Newtonsoft.Json;
using AventStack.ExtentReports;
using SeleniumFramework.Utilities.reportUtil;
using System.Text.RegularExpressions;
//using SeleniumFramework.Utilities.apiUtil;

namespace SeleniumFramework.Pages
{
    public  class APICheck
    {
        public  string LoginAPI(string Scheme, string Authkey)
        {
            string orgId = string.Empty;
            try
            {
                string URL = GlobalVariables.APIURL+"user/";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, Authkey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying Login API"+URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        orgId = GetOrgId(webresponse.Content.ReadAsStringAsync().Result);
                        ExtentReportUtil.report.Log(Status.Pass, "Login API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Login API Unsuccessful returning status:"+webresponse.StatusCode.ToString());
                    }
                }
                return orgId;
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Login API call FAILED"+ex.Message.ToString());
            }
            return orgId;

        }

        public void VersionConfigAPI(string Scheme, string Authkey)
        {
            try
            {
                string URL = GlobalVariables.APIURL + "config/system/version";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, Authkey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying VersionConfigAPI API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "VersionConfigAPI API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "VersionConfigAPI API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "VersionConfigAPI API call FAILED" + ex.Message.ToString());
            }

        }
        
        public string LoginAPI_Invalid(string Scheme, string Authkey)
        {
            string orgId = string.Empty;
            try
            {
                string URL = GlobalVariables.APIURL + "user/";
                Scheme = Scheme + "asg";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, Authkey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying Login API" + URL);
                    if (webresponse.StatusCode != HttpStatusCode.OK)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Login API  returning status NOT Authorized:401" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Login API returning status:" + webresponse.StatusCode.ToString());
                    }
                }
                return orgId;
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Login API call FAILED" + ex.Message.ToString());
            }
            return orgId;

        }

        public string  RetriveSessionAPI(string Scheme,string AuthKey,string SessionId)
        {
            string url = GlobalVariables.APIURL+"session/" + SessionId;
            string orgId = string.Empty;
            try {
                    HttpResponseMessage webresponse = GetWebResponse(Scheme, AuthKey, SessionId,url);
                    ExtentReportUtil.report.Log(Status.Info, "Verifying RetriveSession API" + url);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        orgId = GetOrgId(responseString);
                        ExtentReportUtil.report.Log(Status.Pass, "RetriveSession API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "RetriveSession API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                    return orgId;
                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "RetriveSession API call FAILED" + ex.Message.ToString());
                }
                return orgId;
        }

        public  void RetriveMediaItemAPI()
        {
            string url = GlobalVariables.APIURL+"media-items?session-id=80f77a028a07e4dc74576e42a5cd054f";
            string Scheme = string.Empty;
            string AuthKey = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                try
                {
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(url)).Result;
                    string responseString = webresponse.Content.ReadAsStringAsync().Result;

                    string[] mediaStream = responseString.Replace("\"", "").Split(new string[] { "timestamp:" }, StringSplitOptions.None);
                    int mediacount = mediaStream.Count() - 1;
                    //total media present equal to 
                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "CreateSeesion API call FAILED" + ex.Message.ToString());
                }
            }
        }
        
        public  string CallRestMethod(string url)
        {
            url = "https://9idevtest01.beatsystems.com/eye-service/resource/session";
            //HttpWebClient webClient = new 
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "POST";
            webrequest.ContentType = "application/json";
            //webrequest.PreAuthenticate = true;
            //webrequest.Credentials = 

            webrequest.Headers.Add("Authorization", "cWF0ZXN0MDHCsXNhY2hpbsKxOlJTQTIwNDg6Qm1pdHhaNG03UnZUUnhLOUZyLzFhL2RybWd4WFp1VzUzTXplOXlHNlJjcFBUR3VLQ3djakdoZjVqQ3Y5cWRoY1E3U1BFLzRIZTVkTTFYQVlmYVExamZ0OGs1aXBFNStTaGVndWdyQUR2cTVNVTlueWsxNWEzeXlEMEIwQTZETHZHRnpKTW5HZEh0ZEFxRGcwNzZKaDJDbmcxNEhkT09QTzNtZXlpUGYvV1FiZ3haOE4zamV0cnhwN1g5aDZMQlA0d2FrNmRSYi9NR2w0VnpibHNZRUhJNDl3WlNXa3U5TmhuOVN6czhEQkZuUFhJZnRVaTVRaHpNQUt0eGZmTWJBM1VTZFdxUHhocnJTUlV2WEJpdHJJck5VTCtQR0pkaitXS2RHRGZUR3dLd254Uk1HVFd2akpFN1FqM2dIMlRzbXovTWkrUzlKaCtOWHh5MU9QdVBOeXlBPT0=");
            webrequest.Headers.Add("contactDetails", "yogesh.pawar@capita.co.uk");
            webrequest.Headers.Add("reference", "APICall");
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }

        public  string CreateSessionAPI(string Scheme,string AuthKey)
        {
           string url = GlobalVariables.APIURL+"session";
           string result1 = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                ExtentReportUtil.report.Log(Status.Info, "Verifying Create Session API" + url);
                JObject jOject = new JObject(
                    new JProperty("contactDetails", "yogesh.pawar@capita.co.uk"),
                    new JProperty("reference", "APICall"),
                    new JProperty("parent", "true"));
                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    //   content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    //var stringContent = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(url), content).Result;
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        //string sessionId = responseString.Split("");
                        result1 = GetSessionId(responseString);    
                        ExtentReportUtil.report.Log(Status.Pass, "CreateSeesion API successful returning status OK:200"+webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "CreateSeesion API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                    
                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "CreateSeesion API call FAILED" + ex.Message.ToString());
                }

                return result1;
            }
        }

        public string PostLoginAPI(string Scheme, string AuthKey)
        {
            string url = GlobalVariables.APIURL + "user/login/";
            string result1 = string.Empty;
            string org = string.Empty;
            if (GlobalVariables.URL.Contains("auto"))
            {
                 org = "autoqatesting";
            }
            else
            {
                org = "qatesting";
            }
            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                
                ExtentReportUtil.report.Log(Status.Info, "Verifying PostLoginAPI  API" + url);
                JObject jOject = new JObject(
                    new JProperty("username", "auto_operator"),
                    new JProperty("password", "Password@1"),
                    new JProperty("organisation", org));
                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    //   content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    //var stringContent = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(url), content).Result;
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        string[] token = responseString.Split("");
                        result1 = GetRawToken(responseString);
                        ExtentReportUtil.report.Log(Status.Pass, "PostLoginAPI  successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "PostLoginAPI  Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }

                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "PostLoginAPI  call FAILED" + ex.Message.ToString());
                }

                return result1;
            }
        }

        public void CreateSessionAPI_BadRequest(string Scheme, string AuthKey)
        {
            string url = GlobalVariables.APIURL + "session";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                ExtentReportUtil.report.Log(Status.Info, "Verifying Create Session API" + url);
                JObject jOject = new JObject(
                    new JProperty("contactDetails", ""),
                    new JProperty("reference", "APICall"));
                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    //   content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    //var stringContent = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(url), content).Result;
                    if (webresponse.StatusCode != HttpStatusCode.OK)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "CreateSeesion API  returning status BadRequest:400" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "CreateSeesion API  returning status:" + webresponse.StatusCode.ToString());
                    }

                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "CreateSeesion API call FAILED" + ex.Message.ToString());
                }

                
            }
        }

        public void CreateSessionAPI_Unauthorized(string Scheme, string AuthKey)
        {
            string url = GlobalVariables.APIURL + "session";
            Scheme = Scheme + "asdf";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                ExtentReportUtil.report.Log(Status.Info, "Verifying Create Session API" + url);
                JObject jOject = new JObject(
                    new JProperty("contactDetails", "yogesh.pawar@capita.co.uk"),
                    new JProperty("reference", "APICall"));
                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    //   content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    //var stringContent = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(url), content).Result;
                    if (webresponse.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "CreateSeesion API  returning status InternalServerError:500" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "CreateSeesion API  returning status:" + webresponse.StatusCode.ToString());
                    }

                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "CreateSeesion API call FAILED" + ex.Message.ToString());
                }


            }
        }

        public  List<RootObject> RetriveMediaItemInfo(string Scheme,string AuthKey,string sessionId)
        {
            string responseString = string.Empty;
            List<RootObject> media = null;
            string url = GlobalVariables.APIURL+"media-items?session-id="+sessionId;//be493536d6cff6f7873ac3958fc8c564 + nurl[0];
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();

                    JObject jOject = new JObject(
                        new JProperty("session-id", sessionId));//nurl[0]
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    var task = client.GetAsync(url)
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  media = JsonConvert.DeserializeObject<List<RootObject>>(jsonString.Result);

              });
                    task.Wait();
                    //var dList = new List<dynamic>() { oMycustomclassname };
                    //var str = dList.Select(item => item?.ToString()).ToList();
                    if(!media.Equals(null))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "RetriveMediaItemInfo API successful returning status OK:200");
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "RetriveMediaItemInfo API Unsuccessful returning status:");
                    }
                }
                
                return media;
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "RetriveMediaItemInfo API call FAILED" + ex.Message.ToString());
            }
            return media;
        }

        public void MediaDownload(string Scheme, string AuthKey, string sessionId)
        {
            List<string> id = new List<string>();
            string responseString = string.Empty;
            try
            {
                id = GetMediaItemId(RetriveMediaItemInfo(Scheme, AuthKey, sessionId));

                if (id.Count == 0)
                {
                    ExtentReportUtil.report.Log(Status.Info, "No Media present for given session");
                }
                else
                {
                    string url = GlobalVariables.APIURL+"media-item/"+id[1];
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();

                        //JObject jOject = new JObject(
                        //    new JProperty("session-id", "80f77a028a07e4dc74576e42a5cd054f"));//nurl[0]
                        //var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                        HttpResponseMessage webresponse = client.GetAsync(url).Result;
                        if (CheckStatusCode(webresponse.StatusCode))
                        {
                             responseString = webresponse.Content.ReadAsStringAsync().Result;
                             ExtentReportUtil.report.Log(Status.Pass, "MediaDownload API successful returning status OK:200" + webresponse.StatusCode.ToString());
                        }
                        else
                        {
                            ExtentReportUtil.report.Log(Status.Fail, "MediaDownload API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "MediaDownload API call FAILED" + ex.Message.ToString());
            }
        }

        public void GetSessionByTimeStamp(string Scheme, string AuthKey, string SessionId)
        {
             string url = GlobalVariables.APIURL+"session/" + SessionId;
             string orgId = string.Empty;
            List<int> timestamp = new List<int>();
            try
            {
                HttpResponseMessage webresponse = GetWebResponse(Scheme, AuthKey, SessionId, url);
                ExtentReportUtil.report.Log(Status.Info, "Verifying GetSessionByTimeStamp API" + url);
                
                if (CheckStatusCode(webresponse.StatusCode))
                {
                    string responseString = webresponse.Content.ReadAsStringAsync().Result;
                    orgId = GetOrgId(responseString);
                    timestamp = GetUnixTimestamp();
                    string timeurl = GlobalVariables.APIURL+"sessions?begin-timestamp="+timestamp[0]+"&end-timestamp="+timestamp[1]+"&org-id="+orgId;
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                        client.BaseAddress = new Uri(timeurl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        try
                        {                      
                            webresponse = client.GetAsync(new Uri(timeurl)).Result;
                        }
                        catch (Exception ex)
                        {
                            ExtentReportUtil.report.Log(Status.Fail, " API call FAILED" + ex.Message.ToString());
                        }
                        if (CheckStatusCode(webresponse.StatusCode))
                        {
                            ExtentReportUtil.report.Log(Status.Pass, "GetSessionByTimeStamp API successful returning status OK:200" + webresponse.StatusCode.ToString());
                        }
                        else
                        {
                            ExtentReportUtil.report.Log(Status.Fail, "GetSessionByTimeStamp API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetSessionByTimeStamp API call FAILED" + ex.Message.ToString());
            }
        }

        public void GetReportDataByOrganizationId_TimeRange(string Scheme,string AuthKey,string SessionId)
        {
            string url = GlobalVariables.APIURL + "session/" + SessionId;
            string orgId = string.Empty;
            List<int> timestamp = new List<int>();
            try
            {
                HttpResponseMessage webresponse = GetWebResponse(Scheme, AuthKey, SessionId, url);
                ExtentReportUtil.report.Log(Status.Info, "Verifying GetReportDataByOrganizationId_TimeRange API" + url);

                if (CheckStatusCode(webresponse.StatusCode))
                {
                    string responseString = webresponse.Content.ReadAsStringAsync().Result;
                    orgId = GetOrgId(responseString);
                    timestamp = GetUnixTimestamp();
                    string timeurl = GlobalVariables.APIURL + "report/sessions?begin-timestamp=" + timestamp[0] + "&end-timestamp=" + timestamp[1] + "&org-id=" + orgId;
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                        client.BaseAddress = new Uri(timeurl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        try
                        {
                            webresponse = client.GetAsync(new Uri(timeurl)).Result;
                        }
                        catch (Exception ex)
                        {
                            ExtentReportUtil.report.Log(Status.Fail, " API call FAILED" + ex.Message.ToString());
                        }
                        if (CheckStatusCode(webresponse.StatusCode))
                        {
                            ExtentReportUtil.report.Log(Status.Pass, "GetReportDataByOrganizationId_TimeRange API successful returning status OK:200" + webresponse.StatusCode.ToString());
                        }
                        else
                        {
                            ExtentReportUtil.report.Log(Status.Fail, "GetReportDataByOrganizationId_TimeRange API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetSessionByTimeStamp API call FAILED" + ex.Message.ToString());
            }
        }

        public void GetReportDataByOrganizations_TimeRange(string Scheme, string AuthKey)
        {
            List<int> timestamp = new List<int>();
            HttpResponseMessage webResponse=  null;
            try
            {
                    timestamp = GetUnixTimestampJune();
                    string timeurl = GlobalVariables.APIURL + "report/session?begin-timestamp=" + timestamp[0] + "&end-timestamp=" + timestamp[1];
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                        client.BaseAddress = new Uri(timeurl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        try
                        {
                        webResponse = client.GetAsync(new Uri(timeurl)).Result;
                        }
                        catch (Exception ex)
                        {
                            ExtentReportUtil.report.Log(Status.Fail, " API call FAILED" + ex.Message.ToString());
                        }
                        if (CheckStatusCode(webResponse.StatusCode))
                        {
                            ExtentReportUtil.report.Log(Status.Pass, "GetReportDataByOrganizations_TimeRange API successful returning status OK:200" + webResponse.StatusCode.ToString());
                        }
                        else
                        {
                            ExtentReportUtil.report.Log(Status.Fail, "GetReportDataByOrganizations_TimeRange API Unsuccessful returning status:" + webResponse.StatusCode.ToString());
                        }
                    }
               }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetSessionByTimeStamp API call FAILED" + ex.Message.ToString());
            }
        }

        public void DownloadAuditPDF(string Scheme, string AuthKey, string SessionId)
        {
            string url = GlobalVariables.APIURL+"activities/download-audit-pdf-success?session-id=" + SessionId;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                ExtentReportUtil.report.Log(Status.Info, "Verifying Create Session API" + url);
                JObject jOject = new JObject(
                    new JProperty("session-id", SessionId));
                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(url), content).Result;
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "DownloadAuditPDF API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "DownloadAuditPDF API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }

                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "DownloadAuditPDF API call FAILED" + ex.Message.ToString());
                }
            }
        
}

        public void ReportLogActivity(string Scheme, string AuthKey)
        {
            string url = GlobalVariables.APIURL+"report/download-report-log-activity?orgName=CAPITA CCS";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                ExtentReportUtil.report.Log(Status.Info, "Verifying Create Session API" + url);
                JObject jOject = new JObject(
                    new JProperty("orgName", "CAPITA CCS"));
                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(url), content).Result;
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "ReportLogActivity API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "ReportLogActivity API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }

                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "DownloadAuditPDF API call FAILED" + ex.Message.ToString());
                }
            }
        }

        public void GetOrganisations(string Scheme, string AuthKey)
        {
            try
            {
                string URL = GlobalVariables.APIURL+"organisations";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying GetOrganizations API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        int orgCount = Regex.Matches(responseString, "fullName").Count;
                        ExtentReportUtil.report.Log(Status.Pass, "GetOrganizations API successful returning status OK:200" + webresponse.StatusCode.ToString());
                        ExtentReportUtil.report.Log(Status.Pass, "Total Number of Organisations=" + orgCount);
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "GetOrganizations API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetOrganizations API call FAILED" + ex.Message.ToString());
            }
        }

        public void GetOrganisationConfiguration(string Scheme, string AuthKey)
        {
            try
            {
                string URL = GlobalVariables.APIURL + "organisation/configuration";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying GetOrganisationConfiguration API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "GetOrganisationConfiguration API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "GetOrganisationConfiguration API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetOrganizations API call FAILED" + ex.Message.ToString());
            }
        }

        public void GetOrganisationConfigById(string Scheme, string AuthKey)
        {
            try
            {
                string OrgId = LoginAPI(Scheme, AuthKey);
                string URL = GlobalVariables.APIURL+"organisation/"+OrgId;

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying GetOrganisationConfigById API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        ExtentReportUtil.report.Log(Status.Pass, "GetOrganisationConfigById API successful returning status OK:200" + webresponse.StatusCode.ToString());
                        ExtentReportUtil.report.Log(Status.Pass, "Organisation Name:"+GetOrgName(responseString));
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "GetOrganisationConfigById API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetOrganisationConfigById API call FAILED" + ex.Message.ToString());
            }
        }

        public void GetOrganisationByType(string Scheme, string AuthKey)
        {
            try
            {
                string URL = GlobalVariables.APIURL+"organisation/?type=System Admin";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying GetOrganisationByType API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        ExtentReportUtil.report.Log(Status.Pass, "GetOrganisationByType API successful returning status OK:200" + webresponse.StatusCode.ToString());
                        ExtentReportUtil.report.Log(Status.Pass, "Organisation Name:" + GetOrgName(responseString));
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "GetOrganisationByType API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetOrganisationByType API call FAILED" + ex.Message.ToString());
            }
        }

        public void GetOrganisationsTypes(string Scheme, string AuthKey)
        {
            try
            {
                string URL = GlobalVariables.APIURL+"organisations/types";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying GetOrganisationsTypes API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        int orgTypeCount = responseString.Replace("\"", "").Split(new string[] { "," }, StringSplitOptions.None).Count();
                        ExtentReportUtil.report.Log(Status.Pass, "GetOrganisationsTypes API successful returning status OK:200" + webresponse.StatusCode.ToString());
                        ExtentReportUtil.report.Log(Status.Pass, "Organisation Type Count:" + orgTypeCount);
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "GetOrganisationsTypes API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetOrganisationsTypes API call FAILED" + ex.Message.ToString());
            }
        }

        public void GetOrganisationProperties(string Scheme, string AuthKey)
        {
            try
            {
                string URL = GlobalVariables.APIURL+"organisation/configuration";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying GetOrganisationProperties API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        ExtentReportUtil.report.Log(Status.Pass, "GetOrganisationProperties API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "GetOrganisationsTypes API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetOrganisationsTypes API call FAILED" + ex.Message.ToString());
            }
        }

        public void DeleteOrganisation(string Scheme, string AuthKey)
        {
            string orgId = LoginAPI(Scheme,AuthKey);
            try
            {
                string URL = GlobalVariables.APIURL+"organisation/"+ orgId;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.DeleteAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying DeleteOrganisation API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        ExtentReportUtil.report.Log(Status.Pass, "DeleteOrganisation API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "DeleteOrganisation API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "DeleteOrganisation API call FAILED" + ex.Message.ToString());
            }
        }

        public void PostOrganisation(string Scheme, string AuthKey)
        {
            string orgId = LoginAPI(Scheme, AuthKey);
            try
            {
                string URL = GlobalVariables.APIURL+ "organisation/48fd99c5149f34820689515280edbdcb";
                    JObject jOject = new JObject(
                    new JProperty("orgId", "{,_}"));
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(URL),content).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying DeleteOrganisation API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        ExtentReportUtil.report.Log(Status.Pass, "PostOrganisation API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "PostOrganisation API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "PostOrganisation API call FAILED" + ex.Message.ToString());
            }
        }

        public void PostOrganisationByType(string Scheme, string AuthKey)
        {
            string orgId = LoginAPI(Scheme, AuthKey);
            try
            {
                string URL = GlobalVariables.APIURL + "organisations/?type=System Admin" + orgId;
                JObject jOject = new JObject(
                new JProperty("orgId", orgId));
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(URL), content).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying DeleteOrganisation API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        ExtentReportUtil.report.Log(Status.Pass, "PostOrganisation API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "PostOrganisation API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "PostOrganisation API call FAILED" + ex.Message.ToString());
            }
        }

        public void EndSession(string Scheme, string AuthKey)
        {
            string url = GlobalVariables.APIURL + "session/state/multipleStreams";
            string SessionId = CreateSessionAPI(Scheme, AuthKey);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                ExtentReportUtil.report.Log(Status.Info, "Verifying EndSession  API" + url);
                JObject jOject = new JObject(
                    new JProperty("_id", SessionId),
                    new JProperty("state", "1"));
                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PutAsync(new Uri(url), content).Result;
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "EndSession API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "EndSession" +
                            " API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }

                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "EndSession API call FAILED" + ex.Message.ToString());
                }
            }
        }

        public void UpdateUser(string Scheme1, string AuthKey1, string Scheme, string AuthKey)
        {
            string userId = LoginUserSuccess(Scheme1, AuthKey1);
            string url = GlobalVariables.APIURL + "users/" + userId;
            using (HttpClient client = new HttpClient())
            {
                
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                ExtentReportUtil.report.Log(Status.Info, "Verifying UpdateUser  API" + url);
                JObject jOject = new JObject(
                    new JProperty("username", "updateuser"),
                    new JProperty("forename", "update"),
                    new JProperty("surname", "user23"),
                    new JProperty("emailAddress", "yogesh.pawar@capita.co.uk"),
                    new JProperty("enabled", true),
                    new JProperty("canDownloadMedia", false),
                    new JProperty("canShareStream", false),
                    new JProperty("types", new JArray("OPERATOR")));

                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PutAsync(new Uri(url), content).Result;
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "UpdateUser API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "UpdateUser" +
                            " API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }

                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "UpdateUser API call FAILED" + ex.Message.ToString());
                }
            }
        }

        public void GetSessionMediaCountById(string Scheme, string AuthKey, string SessionId)
        {
            string url = GlobalVariables.APIURL + "media-items/count?session-id=" + SessionId;
            string responseString = string.Empty;
            try
            {
                    HttpResponseMessage webresponse = GetWebResponse(Scheme, AuthKey, SessionId, url);
                    ExtentReportUtil.report.Log(Status.Info, "Verifying GetSessionMediaCountById API" + url);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        responseString = webresponse.Content.ReadAsStringAsync().Result;
                        ExtentReportUtil.report.Log(Status.Pass, "GetSessionMediaCountById API successful returning status OK:200" + webresponse.StatusCode.ToString()+responseString);
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "GetSessionMediaCountById API Unsuccessful returning status:" + webresponse.StatusCode.ToString()+responseString);
                    }
                
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetSessionMediaCountById API call FAILED" + ex.Message.ToString()+responseString);
            }

        }

        public void GetUsersByOrgId(string Scheme, string AuthKey)
        {
            string orgId = LoginAPI(Scheme, AuthKey);
            try
            {
                string URL = GlobalVariables.APIURL + "users?organisation-id="+orgId;

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying GetUsersByOrgId API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        int UserCount = Regex.Matches(responseString, "username").Count;
                        ExtentReportUtil.report.Log(Status.Pass, "GetUsersByOrgId API successful returning status OK:200" + webresponse.StatusCode.ToString());
                        ExtentReportUtil.report.Log(Status.Pass, "Total User Count:" + UserCount);
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "GetUsersByOrgId API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetUsersByOrgId API call FAILED" + ex.Message.ToString());
            }
        }

        public void GetUsers(string Scheme, String AuthKey)
        {
            try
            {
                string URL = GlobalVariables.APIURL + "users";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying GetUsers API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        int UserCount = Regex.Matches(responseString, "username").Count;
                        ExtentReportUtil.report.Log(Status.Pass, "GetUsers API successful returning status OK:200" + webresponse.StatusCode.ToString());
                        ExtentReportUtil.report.Log(Status.Pass, "Total User Count:" + UserCount);
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "GetUsers API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetUsersByOrgId API call FAILED" + ex.Message.ToString());
            }
        }

        public void GetUserById(string Scheme, String AuthKey)
        {
            string userId = LoginUserSuccess(Scheme, AuthKey);
            try
            {
                string URL = GlobalVariables.APIURL + "users/"+ userId;

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                    ExtentReportUtil.report.Log(Status.Info, "Verifying GetUsersById API" + URL);
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        ExtentReportUtil.report.Log(Status.Pass, "GetUsersById API successful returning status OK:200" + webresponse.StatusCode.ToString());
                        ExtentReportUtil.report.Log(Status.Pass, "ResponseString:" + responseString);
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "GetUsersById API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "GetUsersById API call FAILED" + ex.Message.ToString());
            }
        }

        public string LoginUserSuccess(string scheme, string AuthKey)
        {
            string URL = GlobalVariables.APIURL+"activities/login-user-success";
            string orgId = LoginAPI(scheme, AuthKey);
            string userId = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, AuthKey);
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                ExtentReportUtil.report.Log(Status.Info, "LoginUserSuccess API" + URL);
                JObject jOject = new JObject(
                    new JProperty("orgId", orgId));
                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(URL), content).Result;
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        userId = GetUserId(webresponse.Content.ReadAsStringAsync().Result);
                        ExtentReportUtil.report.Log(Status.Pass, "LoginUserSuccess API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "LoginUserSuccess API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }
                    return userId;
                }

                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "LoginUserSuccess API call FAILED" + ex.Message.ToString());
                }
                return userId;
            }

          }

        //public void UpdatePassword(string Scheme1, string AuthKey1, string Scheme, string AuthKey)
        //{
        //    string userId = LoginUserSuccess(Scheme1, AuthKey1);
        //    string url = GlobalVariables.APIURL + "users/"+userId+"/new-password";
        //    //var old = new JSEncrypt();
        //    RSAEncrypt rs = new RSAEncrypt();
        //    string currentpasword = rs.Encrypt("Asdfgh@123");
        //    string newpassword = rs.Encrypt("Mpassword@123");
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        ExtentReportUtil.report.Log(Status.Info, "Verifying UpdatePassword Session API" + url);
        //        JObject jOject = new JObject(
        //            new JProperty("currentPassword","RSA2048:"+ currentpasword),
        //            new JProperty("newPassword", "RSA2048:"+newpassword));
        //        try
        //        {
        //            var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
        //            //   content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
        //            //var stringContent = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
        //            HttpResponseMessage webresponse = client.PostAsync(new Uri(url), content).Result;
        //            if (webresponse.StatusCode==HttpStatusCode.Forbidden)
        //            {
        //                string responseString = webresponse.Content.ReadAsStringAsync().Result;
        //                //string sessionId = responseString.Split("");
        //                ExtentReportUtil.report.Log(Status.Pass, "UpdatePassword API successful returning status Forbidden:403" + webresponse.StatusCode.ToString());
        //            }
        //            else
        //            {
        //                ExtentReportUtil.report.Log(Status.Fail, "UpdatePassword API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            ExtentReportUtil.report.Log(Status.Fail, "UpdatePassword API call FAILED" + ex.Message.ToString());
        //        }

        //    }
        //}

        public void SendSMS(string Scheme, string AuthKey)
        {
            string url = GlobalVariables.APIURL + "sms";
            string UserId = LoginUserSuccess(Scheme,AuthKey);
            string SessionId = CreateSessionAPI(Scheme, AuthKey);
            Guid guid = Guid.NewGuid();
            string roomId = guid.ToString();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                ExtentReportUtil.report.Log(Status.Info, "Verifying SendSMS  API" + url);
                JObject jOject = new JObject(
                    new JProperty("phoneNumber", "9421681290"),
                    new JProperty("referenceNumber", "APICall"),
                    new JProperty("room", roomId),
                    new JProperty("userId", UserId),
                    new JProperty("sessionId", SessionId));
                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    //   content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    //var stringContent = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(url), content).Result;
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        //string sessionId = responseString.Split("");
                        ExtentReportUtil.report.Log(Status.Pass, "SendSMS API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "SendSMS API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }

                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "SendSMS API call FAILED" + ex.Message.ToString());
                }

            }
        }

        public void SendMessage(string Scheme, string AuthKey)
        {
            string url = GlobalVariables.APIURL + "message";
            string UserId = LoginUserSuccess(Scheme, AuthKey);
            string SessionId = CreateSessionAPI(Scheme, AuthKey);
            Guid guid = Guid.NewGuid();
            string roomId = guid.ToString();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                ExtentReportUtil.report.Log(Status.Info, "Verifying SendSMS  API" + url);
                JObject jOject = new JObject(
                    new JProperty("referenceNumber", "APICall"),
                    new JProperty("room", roomId),
                    new JProperty("userId", UserId),
                    new JProperty("sessionId", SessionId));
                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    //   content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    //var stringContent = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(url), content).Result;
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        //string sessionId = responseString.Split("");
                        ExtentReportUtil.report.Log(Status.Pass, "SendSMS API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "SendSMS API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }

                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "SendSMS API call FAILED" + ex.Message.ToString());
                }

            }
        }

        public void SendEmail(string Scheme, string AuthKey)
        {
            string url = GlobalVariables.APIURL + "email";
            string UserId = LoginUserSuccess(Scheme, AuthKey);
            string SessionId = CreateSessionAPI(Scheme, AuthKey);
            Guid guid = Guid.NewGuid();
            string roomId = guid.ToString();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                ExtentReportUtil.report.Log(Status.Info, "Verifying SendEmail  API" + url);
                JObject jOject = new JObject(
                    new JProperty("referenceNumber", "APICall"),
                    new JProperty("room", roomId),
                    new JProperty("userId", UserId),
                    new JProperty("sessionId", SessionId));
                try
                {
                    var content = new StringContent(jOject.ToString(), Encoding.UTF8, "application/json");
                    //   content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    //var stringContent = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
                    HttpResponseMessage webresponse = client.PostAsync(new Uri(url), content).Result;
                    if (CheckStatusCode(webresponse.StatusCode))
                    {
                        string responseString = webresponse.Content.ReadAsStringAsync().Result;
                        //string sessionId = responseString.Split("");
                        ExtentReportUtil.report.Log(Status.Pass, "SendEmail API successful returning status OK:200" + webresponse.StatusCode.ToString());
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "SendEmail API Unsuccessful returning status:" + webresponse.StatusCode.ToString());
                    }

                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "SendEmail API call FAILED" + ex.Message.ToString());
                }

            }
        }

        private bool CheckStatusCode(HttpStatusCode code)
        {
            if (code.Equals(HttpStatusCode.OK))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetSessionId(string responseString)
        {
            string[] sessionString = responseString.Replace("\"", "").Split(new string[] { "session-id=" }, StringSplitOptions.None);
            string[] sessionId = sessionString[1].Split(',');
            return sessionId[0];
        }

        private string GetUserId(string responseString)
        {
            string[] userString = responseString.Replace("\"", "").Split(new string[] { "userId:" }, StringSplitOptions.None);
            string[] userId = userString[1].Split(',');
            return (userId[0]);
        }

        private string GetOrgId(string responseString)
        {
            string[] orgString = responseString.Replace("\"", "").Split(new string[] { "organisationId:" }, StringSplitOptions.None);
            string[] orgId = orgString[1].Split(',');
            return orgId[0];
        }

        private string GetOrgName(string responseString)
        {
            string[] orgString = responseString.Replace("\"", "").Split(new string[] { "fullName:" }, StringSplitOptions.None);
            string[] orgId = orgString[1].Split(',');
            return orgId[0];
        }

        private string GetRawToken(string responseString)
        {
            string[] rawTokenString = responseString.Replace("\"", "").Split(new string[] { "rawToken:" }, StringSplitOptions.None);
            string[] tokenNumber = rawTokenString[1].Split(',');
            return tokenNumber[0];
        }

        private List<string> GetMediaItemId(List<RootObject> media)
        {
            List<string> id = new List<string>();
            foreach (RootObject r in media)
            {
                id.Add(r._id);
            }
            return id;
        }

        private HttpResponseMessage GetWebResponse(string Scheme, string AuthKey, string sessionId, string url)
        {

            HttpResponseMessage webresponse = null;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, AuthKey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                try
                {
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
                    webresponse = client.GetAsync(new Uri(url)).Result;
                }
                catch (Exception ex)
                {
                    ExtentReportUtil.report.Log(Status.Fail, " API call FAILED" + ex.Message.ToString());
                }

            }
            return webresponse;
        }

        private  List<int> GetUnixTimestamp()
        {
            List<int> timestamp = new List<int>();
            DateTime EnddateTime = new DateTime(2019, 10, 18, 2, 20, 0);
            DateTime StartTime = new DateTime(2019, 4, 1, 3, 12, 0);
            timestamp.Add((Int32)(StartTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds));
            timestamp.Add((Int32)(EnddateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
            return timestamp;
        }

        private List<int> GetUnixTimestampJune()
        {
            List<int> timestamp = new List<int>();
            DateTime StartTime = new DateTime(2019, 6, 1, 1, 1, 0);
            timestamp.Add((Int32)(StartTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds));
            timestamp.Add((Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
            return timestamp;
        }


    }


    public class Info
    {
        public string deviceType { get; set; }
        public string viewSessionUrl { get; set; }
        public object compassDirection { get; set; }
        public string deviceVendor { get; set; }
        public string latitude { get; set; }
        public string accuracy { get; set; }
        public string startSessionUrl { get; set; }
        public string osName { get; set; }
        public string rawUaString { get; set; }
        public string osVersion { get; set; }
        public string browserVersion { get; set; }
        public string browserName { get; set; }
        public string deviceModel { get; set; }
        public string longitude { get; set; }
    }

    public class VideoWebm
    {
        public bool stub { get; set; }
        public int revpos { get; set; }
        public string digest { get; set; }
        public string content_type { get; set; }
    }

    public class Attachments
    {
        public VideoWebm __invalid_name__video { get; set; }
    }

    public class RootObject
    {
        public object timestamp { get; set; }
        public string userId { get; set; }
        public string sessionId { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public int sizeInBytes { get; set; }
        public string digest { get; set; }
        public Info info { get; set; }
        public bool viewingRestricted { get; set; }
        public string _id { get; set; }
        public string _rev { get; set; }
        public Attachments _attachments { get; set; }
        public int mediaIndexNumber { get; set; }
    }

    public enum type
    {
        OPERATOR
    }
}
