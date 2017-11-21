using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote_Staff_Modules
{
    class DevelopmentSettings : ISettings
    {
        public string APIURL()
        {
            return "http://test.njs.remotestaff.com.au";
        }

        public string LoginCouchDb()
        {
            return "logins";
        }

        public string SubcontractorDetailsDb()
        {
            return "subcontractor_details";
        }

        public string TimeblocksDb()
        {
            return "time_blocks";
        }

        public string WorkDb()
        {
            return "work";
        }
    }
}
