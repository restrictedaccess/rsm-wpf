using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Lite;
using Remote_Staff_Modules;
using Models;

namespace RSM
{
    public class CouchBaseReplicatorTimeBlock : CouchBaseReplicatorAbstract
    {
        public override void InitializeReplication(LoginModule module)
        {
            List<string> filters = new List<string>();
            foreach (Subcontractor contract in module.LoggedInStaff.Contracts)
            {
                Console.WriteLine("Subcon Id : " + contract.Id);
                filters.Add("time_blocks_" + contract.Id + "");
            }
            Replicate(module.GetSettings().TimeblocksDb(), module.GetSettings().TimeblocksDb(), filters);
        }

        protected override void PullChanged(object sender, ReplicationChangeEventArgs e)
        {
            if (!IsProcessing(sender, e))
            {

            }
        }
    }
}
