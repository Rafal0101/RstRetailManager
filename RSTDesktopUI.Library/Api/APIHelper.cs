using RSTDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RSTDesktopUI.Library.Api
{

    public class APIHelper : IAPIHelper
    {
        private HttpClient ApiCLient;
        private ILogedInUserModel _logedInUserModel;

        public APIHelper(ILogedInUserModel logedInUserModel)
        {
            InitializeClient();
            _logedInUserModel = logedInUserModel;
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

        public async Task GetLoggedInUserInfo(string token)
        {
            ApiCLient.DefaultRequestHeaders.Clear();
            ApiCLient.DefaultRequestHeaders.Accept.Clear();
            ApiCLient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiCLient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await ApiCLient.GetAsync("/api/User"))
            {
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LogedInUserModel>();
                    _logedInUserModel.CreatedDate = result.CreatedDate;
                    _logedInUserModel.EmailAddress = result.EmailAddress;
                    _logedInUserModel.FirstName = result.FirstName;
                    _logedInUserModel.LastName = result.LastName;
                    _logedInUserModel.Id = result.Id;
                    _logedInUserModel.Token = token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
