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
    public class CouchBaseReplicatorWork : CouchBaseReplicatorAbstract
    {
        public override void InitializeReplication(LoginModule module)
        {
            
            List<string> filters = new List<string>();
            foreach(Subcontractor contract in module.LoggedInStaff.Contracts)
            {
                filters.Add("work_" + contract.Id + "");
            }

            this.Replicate(module.GetSettings().WorkDb(), module.GetSettings().WorkDb(), filters);
        }

        protected override void PullChanged(object sender, ReplicationChangeEventArgs e)
        {
            if (!IsProcessing(sender, e))
            {
                
            }
        }


    }
}
