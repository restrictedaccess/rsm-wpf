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
    class CouchbaseLiteSaveTimeBlocks : ICouchSave
    {
        public bool save(IProcessData data, ISettings settings)
        {

            TimeBlockData timeBlockData = (TimeBlockData)data;

            var properties = timeBlockData.TimeBlock.ToJSON();
            //Console.WriteLine(properties);

            var manager = Manager.SharedInstance;
            var timeblocksDb = manager.GetDatabase(settings.TimeblocksDb());

            //Create new Document
            var document = timeblocksDb.CreateDocument();
            var revision = document.PutProperties(properties);
            var docId = document.Id;

            try
            {
                //Attach ScreenShot
                var couchDoc = timeblocksDb.GetDocument(docId);
                var newRev = couchDoc.CurrentRevision.CreateRevision();
                newRev.SetAttachment("screenshot" + docId + ".jpg", "image/jpeg", timeBlockData.TimeBlock.ScreenShot);
                newRev.Save();
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR : " + e);
            }
            
            

            Console.WriteLine("Document created with ID = {0}", docId);
            return true;

        }
    }
}
