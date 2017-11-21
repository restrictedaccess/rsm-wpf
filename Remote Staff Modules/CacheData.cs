using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Remote_Staff_Modules
{
    public class CacheData : IProcessData
    {
        private string userid;
        private Staff loggedInStaff;

        public string Userid
        {
            get
            {
                return userid;
            }
            set
            {
                userid = value;
            }
        }

        public Staff LoggedInStaff
        {
            get
            {
                return loggedInStaff;
            }

            set
            {
                loggedInStaff = value;
            }
        }

        public List<string> GetErrors()
        {
            List<string> errors = new List<string>();
            errors.Add("Staff userid is missing.");
            return errors;
        }

        public FormUrlEncodedContent GetFormData()
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("userid", userid)
            });
        }

        public bool Validate()
        {
            if (userid == string.Empty)
            {
                return false;
            }

            return true;
        }
    }
}
