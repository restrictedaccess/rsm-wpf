using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote_Staff_Modules
{
    public interface ICouchSave
    {
        bool save(IProcessData data, ISettings settings);
    }
}
