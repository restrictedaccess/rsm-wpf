using System;
using System.Threading.Tasks;
using System.Diagnostics;
using jParser;
using Models;

namespace Remote_Staff_Modules
{
    class VersionModule : BaseModule
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
                //Console.WriteLine("Error");
                Debug.WriteLine(processData.GetErrors().Count);
                foreach (string error in processData.GetErrors())
                {
                    Debug.WriteLine(error);
                }

                return false;
            }
            VersionData versionData = (VersionData)processData;

            Task<string> taskResult =  Post(versionData.GetFormData(), "/rsm/version/");
            string result = await taskResult;
            dynamic json = jParser.Parser.Parse(result);
            bool success = json["success"];
            Debug.WriteLine("VersionModule : " + success);
            RESULT = json;
            return  success;

        }

        public override Task<bool> Process(IProcessData processData, ICouchSave couchSave)
        {
            throw new NotImplementedException();
        }
    }
}
