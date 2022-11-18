using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;

namespace VUPayRoll.WebApp
{
    public static class Utility
    {
        internal const int SessionTimeout = 20;
        //internal const string APIBaseAddress = "http://vuprapi.vu360solutions.org";
        internal const string APIBaseAddress = "http://localhost:50658";

        public static HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(APIBaseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (HttpContext.Current.Session["UserToken"] != null)
            {
                client.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["UserToken"].ToString());
            }
            return client;
        }
    }
}