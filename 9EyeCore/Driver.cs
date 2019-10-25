using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace _9EyeCore
{
    public class Driver
    {
        //public static IWebDriver driver;

        public string APICheck()
        {
            string orgId = string.Empty;
            try
            {
                string URL = "https://9idevtest01.eye999.co.uk/eye-service/resource/user/";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("cWF0Z", "XN0aW5nwrFhdXRvX2FkbWluwrE6UlNBMjA0ODoyWHdBd3llS0phR1krNGRDdk9FTnVqQmo5NzkvNEF1WFEwRmoweHpwQTJiSG9Ib0NkaEFLQWI1SDVxU3pmUXQ4Sy9WTktaYWhOb1pZVWt0Q2FhZEJNd00vWUo0M0ZWcm8vRlRPSnYyYmpUWUpqSENadEkyaEF4NkROYldmV0pTN21tL1c0SVFLY2w1UmJ6bzZnWHRkWEN4V1hCMlQ2N2Y1aEJuSndBbnZGVk9IUTN3NlYvSFEzYWRGTzdZQkdQb3RQbmloR0RPSXovY042dmlRR2FSY2YrU2xRTFU5eEI3R2t4VjczaTVGM0k4bldqVXAvZjV2Q2Z4K2lNK1NFeFFEeHNRZVFqTHAzeHUrVUZ1aGNTYlpmRjRsSzJxdDA1VkIzZndQckpwQTA2SVhyVHFYMUo3TVJNczlSK1dlVlZ0TkZyNEZEUElGZ1hCUUNaNlJzSGN2Znc9PQ==");
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json")
                    HttpResponseMessage webresponse = client.GetAsync(new Uri(URL)).Result;
                   
                }
                return orgId;
            }
            catch (Exception ex)
            {
                
            }
            return orgId;
        }
    }
}
