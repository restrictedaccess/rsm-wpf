using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote_Staff_Modules
{
    public interface ISettings
    {
        string APIURL();
        string LoginCouchDb();

        string SubcontractorDetailsDb();

        string TimeblocksDb();

        string WorkDb();


    }
}
