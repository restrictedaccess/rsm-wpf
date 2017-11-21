using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Mouse : Activity
    {

        //Mouse click. Either Left Click or Right Click
        private string mouseButton;

        public string MouseButton
        {
            get
            {
                return mouseButton;
            }

            set
            {
                mouseButton = value;
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
                {"mouse_button", mouseButton},
                {"activity_event", this.ActivityEvent },
                {"date_activity", this.DateActivity.ToString("o") }
            };

            return properties;
        }
    }
}
