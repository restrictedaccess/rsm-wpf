using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Activity : IProxyModel
    {
        //Date and Time captured
        private DateTime dateActivity;

        /*Keyboard and Mouse events
         
         //Keyboard
         KeyDown
         KeyUp
         KeyPress

         //Mouse
         MouseClick
         MouseDown
         MouseMove
         MouseUp
         MouseDoubleClick
         MouseDragStarted
         MouseDragFinished
        */
        private string activityEvent;


        public DateTime DateActivity
        {
            get
            {
                return dateActivity;
            }

            set
            {
                dateActivity = value;
            }
        }

        public string ActivityEvent
        {
            get
            {
                return activityEvent;
            }

            set
            {
                activityEvent = value;
            }
        }

        public abstract IProxyModel convert(dynamic jsonObject);
        

        public abstract dynamic ToJSON();
        
    }
}
