using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Diagnostics;

namespace Remote_Staff_Modules
{
    class TimeBlockModule : BaseModule
    {
        public override Task<bool> Process()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Process(ICouchSave couchSave)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Process(IProcessData processData)
        {
            throw new NotImplementedException();
        }

        public async override Task<bool> Process(IProcessData processData, ICouchSave couchSave)
        {
            if (!processData.Validate())
            {
                Debug.WriteLine("Error");
                return false;
            }

            TimeBlockData timeBlockData = (TimeBlockData)processData;
            couchSave.save(timeBlockData, GetSettings());
            return true;
        }
    }
}
