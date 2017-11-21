using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class TimeBlock : IProxyModel
    {
        private DateTime start;

        private DateTime end;

        private MemoryStream screenShot;
        

        private int screenShotCapturedSecond;

        private DateTime lastActivityDateTime;

        private List<Activity> activities = new List<Activity>();

        private string status;

        private List<string> processes = new List<string>();

        private Work work;

        private Subcontractor subcon;

        public DateTime Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }

        public DateTime End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
            }
        }

        public MemoryStream ScreenShot
        {
            get
            {
                return screenShot;
            }

            set
            {
                screenShot = value;
            }
        }

        public int ScreenShotCapturedSecond
        {
            get
            {
                return screenShotCapturedSecond;
            }

            set
            {
                screenShotCapturedSecond = value;
            }
        }

        public DateTime LastActivityDateTime
        {
            get
            {
                return lastActivityDateTime;
            }

            set
            {
                lastActivityDateTime = value;
            }
        }

        public List<Activity> Activities
        {
            get
            {
                return activities;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public List<string> Processes
        {
            get
            {
                return processes;
            }
        }

        public Work Work
        {
            get
            {
                return work;
            }

            set
            {
                work = value;
            }
        }

        public Subcontractor Subcon
        {
            get
            {
                return subcon;
            }

            set
            {
                subcon = value;
            }
        }

        public IProxyModel convert(dynamic jsonObject)
        {
            throw new NotImplementedException();
        }

        public dynamic ToJSON()
        {
            
            List<IDictionary> activitiesJson = new List<IDictionary>();

            foreach (Activity activity in this.activities)
            {
                activitiesJson.Add(activity.ToJSON());
            }

            var properties = new Dictionary<string, object>()
            {
                {"start", start.ToString("o")},
                {"end", end.ToString("o") },                
                {"activities" ,  activitiesJson},
                {"status", Status },
                {"work_id" , Work.DocId },
                {"subcon_id" , Subcon.Id.ToString() },
            };

            return properties;
        }
    }
}
