using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Remote_Staff_Modules
{
    public class StartWorkData : IProcessData
    {

        private Subcontractor subcon;
        private Work work;
        

        public Work Work
        {
            get
            {
                return work;
            }

            set
            {
                work = value;
            }
        }

        public Subcontractor Subcon
        {
            get
            {
                return subcon;
            }

            set
            {
                subcon = value;
            }
        }

        public List<string> GetErrors()
        {
            List<string> errors = new List<string>();
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
                new KeyValuePair<string, string>("subcon_id", Subcon.Id.ToString())            
            });
        }
    }
}
