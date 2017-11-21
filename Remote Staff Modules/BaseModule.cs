using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Remote_Staff_Modules
{
    public abstract class BaseModule
    {
        
        protected dynamic RESULT;


        public dynamic GetResult()
        {
            return RESULT;
        }

        protected async Task<string> Post(FormUrlEncodedContent postData, string serviceURI)
        {
            string baseUrl = this.GetSettings().APIURL();
            HttpClient client = new HttpClient();            
            string url = baseUrl + serviceURI;
            HttpResponseMessage response = await client.PostAsync(new Uri(url), postData);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();             
            return responseBody;
        }

        public abstract Task<bool> Process(IProcessData processData);

        public abstract Task<bool> Process();

        public abstract Task<bool> Process(IProcessData processData, ICouchSave couchSave);

        public abstract Task<bool> Process(ICouchSave couchSave);

        public ISettings GetSettings()
        {
            return new DevelopmentSettings();
        }


    }
}
