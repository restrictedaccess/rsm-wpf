using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Remote_Staff_Modules
{
    public class TimeBlockData : IProcessData
    {

        private TimeBlock timeBlock;

        public TimeBlock TimeBlock
        {
            get
            {
                return timeBlock;
            }

            set
            {
                timeBlock = value;
            }
        }

        public List<string> GetErrors()
        {
            return new List<string>();
        }

        public FormUrlEncodedContent GetFormData()
        {
            throw new NotImplementedException();
        }

        public bool Validate()
        {
           return true;
        }
    }
}
