using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Models;

namespace Remote_Staff_Modules
{
    class CacheModule : BaseModule
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
            if (!processData.Validate())
            {
                Debug.WriteLine(processData.GetErrors().Count);
                foreach (string error in processData.GetErrors())
                {
                    Debug.WriteLine(error);
                }

                return false;
            }

            CacheData cacheData = (CacheData)processData;
           
            
            
            return true;
        }

        public override Task<bool> Process(IProcessData processData, ICouchSave couchSave)
        {
            throw new NotImplementedException();
        }
    }
}
