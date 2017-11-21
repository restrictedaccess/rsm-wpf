using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Lite;
using Couchbase.Lite.Auth;
using Remote_Staff_Modules;

namespace RSM
{
    public abstract class CouchBaseReplicatorAbstract
    {
        protected string username = "rsm";
        protected string password = "pogiako143";
        protected string syncGateway = "http://devs.remotestaff.com.au:4984";

        protected Database database;
        protected Replication pull;
        protected Replication push;

        protected abstract void PullChanged(object sender, ReplicationChangeEventArgs e);

        public abstract void InitializeReplication(LoginModule module);

        
        protected void Replicate(string localDbName, string remoteDbName, List<string> channels)
        {
            var url = new Uri(syncGateway + "/"+remoteDbName+"/");
            database = Manager.SharedInstance.GetDatabase(localDbName);

            pull = database.CreatePullReplication(url);
            push = database.CreatePushReplication(url);

            var auth = AuthenticatorFactory.CreateBasicAuthenticator(this.username, this.password);

            //Pull process
            pull.Channels = channels;
            pull.Authenticator = auth;
            pull.Continuous = true;
            pull.Changed += PullChanged;
            pull.Start();

            //Push process
            push.Authenticator = auth;
            push.Continuous = true;            
            push.Start();
        }


        protected bool IsProcessing(object sender, ReplicationChangeEventArgs e)
        {
            if (pull.Status == ReplicationStatus.Active)
            {
                Console.WriteLine("Sync in progress");
                return true;
            }
            else if (e.LastError != null)
            {
                Exception error = e.LastError;
                if (error is HttpResponseException)
                {
                    HttpResponseException exception = (HttpResponseException)error;
                    if ((int)exception.StatusCode == 401)
                    {
                        Console.WriteLine("Authentication error");                        
                    }
                }

                return true;
            }
            else
            {                              
                return false;
            }

            
        }

    }
}
