using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Remote_Staff_Modules
{
    public class LoginData : IProcessData
    {

        private string emailAddress;
        private string password;

        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }

            set
            {
                emailAddress = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }



        public List<string> GetErrors()
        {
            List<string> errors = new List<string>();

            if (emailAddress == string.Empty)
            {
                errors.Add("Email Address cannot be empty or null");
            }


            if (password == string.Empty)
            {
                errors.Add("Password cannot be empty or null");
            }

            return errors;
        }

        public bool Validate()
        {
            if (this.GetErrors().Count > 0)
            {
                return false;
            }
            return true;
        }

        

        public FormUrlEncodedContent GetFormData()
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", emailAddress),
                new KeyValuePair<string, string>("password", password)
            });
        }

        
    }
}
