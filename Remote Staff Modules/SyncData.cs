using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Remote_Staff_Modules
{
    public class SyncData : IProcessData
    {

        private Work currentWork;

        public Work CurrentWork
        {
            get
            {
                return currentWork;
            }

            set
            {
                currentWork = value;
            }
        }

        public List<string> GetErrors()
        {
            return new List<string>();
        }

        public FormUrlEncodedContent GetFormData()
        {
            return null;
        }

        public bool Validate()
        {
            return true;
        }




    }
}
