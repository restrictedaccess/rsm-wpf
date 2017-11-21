using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Remote_Staff_Modules
{
    public interface IProcessData
    {
        bool Validate();
        //string[] GetErrors();

        List<string> GetErrors();

        FormUrlEncodedContent GetFormData();
    }
}
