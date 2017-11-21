using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Lite;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Models;

namespace Remote_Staff_Modules
{
    class StartWorkModule : BaseModule
    {


        public async override Task<bool> Process()
        {            
            Work work = new Work();
            RESULT = work;
            work.Start = DateTime.Today;

            return true;
        }

        public async override Task<bool> Process(ICouchSave couchSave)
        {
            var result = await Process();
            StartWorkData startData = new StartWorkData();
            startData.Work = RESULT;
            couchSave.save((IProcessData)startData, GetSettings());
            return true;
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

            var settings = GetSettings();
            // TODO Get the source From API and/or CouchLite DB
            var manager = Manager.SharedInstance;
            var database = manager.GetDatabase(settings.SubcontractorDetailsDb());

            StartWorkData startWorkData = (StartWorkData)processData;
            Task<string> taskResult = Post(startWorkData.GetFormData(), "/rsm/start-work/");
            string result = await taskResult;
            dynamic json = JObject.Parse(result);
            bool success = json.success;
            Debug.WriteLine(success);            
            return success;

            
        }

        public override async Task<bool> Process(IProcessData processData, ICouchSave couchSave)
        {
            var result = await Process();
            StartWorkData startData = (StartWorkData)processData;
            startData.Work = RESULT;
            startData.Work.Subcon = startData.Subcon;
            couchSave.save((IProcessData)startData, GetSettings());
            return true;
        }
    }
}
