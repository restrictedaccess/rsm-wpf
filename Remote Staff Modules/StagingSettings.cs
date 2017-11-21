using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote_Staff_Modules
{
    class StagingSettings : ISettings
    {
        public string APIURL()
        {
            return "http://staging.njs.remotestaff.com.au";
        }

        public string LoginCouchDb()
        {
            return "logins";
        }

        public string SubcontractorDetailsDb()
        {
            return "subcontractorDetails";
        }

        public string TimeblocksDb()
        {
            return "timeBlocks";
        }

        public string WorkDb()
        {
            return "work";
        }
    }
}
