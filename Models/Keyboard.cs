using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Keyboard : Activity
    {

        //Keyboard stroke keys
        private string keys;         

        public string Keys
        {
            get
            {
                return keys;
            }

            set
            {
                keys = value;
            }
        }

        public override IProxyModel convert(dynamic jsonObject)
        {
            throw new NotImplementedException();
        }



        public override dynamic ToJSON()
        {
            
            var properties = new Dictionary<string, object>()
            {
                {"keys", keys},
                {"activity_event", this.ActivityEvent },
                {"date_activity", this.DateActivity.ToString("o") }
            };

            return properties;
        }
    }
}
