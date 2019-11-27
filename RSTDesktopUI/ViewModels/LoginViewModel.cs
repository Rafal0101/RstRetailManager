using Caliburn.Micro;
using RSTDesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSTDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private IAPIHelper _apiHelper;

        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


        private string _userName = "tim@iamtimcorey.com";

        public string UserName
        {
            get { return _userName; }
            set { 
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        private string _password = "Pwd12345.";

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool IsErrorVisible
        {
            get 
            {
                bool output = false;
                if(ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output; 
            }
           
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set 
            {
                _errorMessage = value; 
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }


        public bool CanLogIn
        {
            get
            {
                bool output = true;
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                var result = await _apiHelper.Authenticate(UserName, Password);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
