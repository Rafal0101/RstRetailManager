﻿using RSTDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RSTDesktopUI.Helpers
{

    public class APIHelper : IAPIHelper
    {
        private HttpClient ApiCLient;

        public APIHelper()
        {
            InitializeClient();
        }
        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];
            ApiCLient = new HttpClient();
            ApiCLient.BaseAddress = new Uri(api);
            ApiCLient.DefaultRequestHeaders.Accept.Clear();
            ApiCLient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            using (HttpResponseMessage response = await ApiCLient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
