using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Remote_Staff_Modules
{
    public class VersionData : IProcessData
    {
        private string version;

        public string CurrentVersion
        {
            get
            {
                return version;
            }

            set
            {
                version = value;
            }
        }


        public bool Validate()
        {
            if (version == string.Empty)
            {
                return false;
            }

            return true;
        }

        public List<string> GetErrors()
        {
            List<string> errors = new List<string>();
            errors.Add("Outdated version");
            return errors;
        }

        public FormUrlEncodedContent GetFormData()
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("version", version)
            });
        }
    }

}
