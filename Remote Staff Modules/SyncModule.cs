using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Lite;
using System.Diagnostics;
using Models;

namespace Remote_Staff_Modules
{
    class SyncModule : BaseModule
    {
        public override Task<bool> Process()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Process(ICouchSave couchSave)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Process(IProcessData processData)
        {
            return true;
        }

        public override async Task<bool> Process(IProcessData processData, ICouchSave couchSave)
        {
            if (!processData.Validate())
            {
                Debug.WriteLine("Error");
                return false;
            }

            SyncData syncData = (SyncData)processData;
            couchSave.save(syncData, GetSettings());
            return true;
        }
    }
}
