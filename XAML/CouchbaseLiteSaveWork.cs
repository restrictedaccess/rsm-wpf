using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Remote_Staff_Modules;
using Models;
using Couchbase.Lite;

namespace RSM
{
    class CouchbaseLiteSaveWork : ICouchSave
    {
        public bool save(IProcessData data, ISettings settings)
        {
            StartWorkData startData = (StartWorkData)data;

            var properties = startData.Work.ToJSON();
            var manager = Manager.SharedInstance;
            var workDb = manager.GetDatabase(settings.WorkDb());
            var document = workDb.CreateDocument();
            var revision = document.PutProperties(properties);
            var docId = document.Id;
            startData.Work.DocId = docId;
            return true;
        }
    }
}
