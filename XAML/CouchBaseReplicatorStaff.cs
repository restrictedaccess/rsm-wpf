using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Remote_Staff_Modules;
using Couchbase.Lite;
using Couchbase.Lite.Auth;
using Models;
using Newtonsoft.Json.Linq;

namespace RSM
{
    public class CouchBaseReplicatorStaff : CouchBaseReplicatorAbstract
    {

        //configs   
        protected override void PullChanged(object sender, ReplicationChangeEventArgs e)
        {
            if(!IsProcessing(sender, e))
            {
                //Start Syncing Couchbase Document to Objects               
                Staff staff = RemoteStaffSystem.GetInstance().LoginModule.LoggedInStaff;
                syncContract(staff);
                Console.WriteLine("Done Syncing!");
            }
            
        }


        public void syncContract(Staff staff)
        {

            try
            {
                var doc = database.GetDocument(staff.Id + "");

                staff.FirstName = doc.GetProperty("firstName").ToString();
                staff.LastName = doc.GetProperty("lastName").ToString();
                staff.Id = Int32.Parse(doc.GetProperty("userid").ToString());

                foreach (JObject contractDoc in (JArray)doc.GetProperty("contracts"))
                {
                    //look for any contract with the same contract id then update or add if necessary
                    Subcontractor contract = null;
                    bool foundSubcontractorDetail = false;
                    foreach (Subcontractor lookupSubcon in staff.Contracts)
                    {
                        if (lookupSubcon.Id == (int)contractDoc.GetValue("subcontractors_id"))
                        {
                            foundSubcontractorDetail = true;
                            contract = lookupSubcon;
                            break;
                        }
                    }
                    if (!foundSubcontractorDetail)
                    {
                        contract = new Subcontractor();
                    }

                    JObject contractSubcontractorDetail = (JObject)contractDoc.GetValue("subcontractors_detail");
                    contract.StaffRate = (float)contractSubcontractorDetail.GetValue("php_monthly");
                    contract.ClientPrice = (float)contractSubcontractorDetail.GetValue("client_price");
                    contract.StaffEmail = (string)contractSubcontractorDetail.GetValue("staff_email");
                    contract.Id = (int)contractDoc.GetValue("subcontractors_id");
                    contract.JobDesignation = (string)contractSubcontractorDetail.GetValue("job_designation");
                    JObject leadsDetail = (JObject)contractDoc.GetValue("leads_detail");
                    Client client = new Client();
                    client.Id = (int)leadsDetail.GetValue("id");
                    client.FirstName = (string)leadsDetail.GetValue("fname");
                    client.LastName = (string)leadsDetail.GetValue("lname");
                    contract.Client = client;
                    if (!foundSubcontractorDetail)
                    {
                        staff.Contracts.Add(contract);
                    }
                }

                /*
                //var doc = database.GetDocument(staff.Id + "");
                doc.Update((UnsavedRevision newRevision) =>
                {
                    var properties = newRevision.Properties;
                    properties["firstName"] = "Norman";
                    properties["lastName"] = "Pogi";
                    return true;
                });
                */

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR : " + e);
            }


            

        }

        public override void InitializeReplication(LoginModule module)
        {
            List<string> filters = new List<string>();
            filters.Add("user_" + module.LoggedInStaff.Id + "");
            this.Replicate("user_info_" + module.LoggedInStaff.Id, "staff", filters);
        }

        

    }
}
