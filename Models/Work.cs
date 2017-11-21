using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Work : IProxyModel
    {
        private DateTime start;

        private DateTime end;

        private List<TimeBlock> timeBlocks = new List<TimeBlock>();

        private TimeBlock currentTimeBlock;

        private int currentScreenShotTimer = 0;

        private TimeBlock previousTimeBlock;

        private int timeBlockLength = 3;

        private string docId;

        private Subcontractor subcon;    



        public List<TimeBlock> TimeBlocks
        {
            get
            {
                return timeBlocks;
            }
           
        }

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

        public TimeBlock CurrentTimeBlock
        {
            get
            {
                return currentTimeBlock;
            }            
        }

        public TimeBlock PreviousTimeBlock
        {
            get
            {
                return previousTimeBlock;
            }

            set
            {
                previousTimeBlock = value;
            }
        }

        public int TimeBlockLength
        {
            get
            {
                return timeBlockLength;
            }

            set
            {
                timeBlockLength = value;
            }
        }

        public string DocId
        {
            get
            {
                return docId;
            }

            set
            {
                docId = value;
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

        public TimeBlock CreateTimeBlock()
        {
            ResetScreenShotTimer();
            if (currentTimeBlock != null)
            {
                PreviousTimeBlock = currentTimeBlock;
            }

            TimeBlock timeblock = new TimeBlock();
            timeblock.Start = DateTime.Today;
            TimeBlocks.Add(timeblock);
            currentTimeBlock = timeblock;
            timeblock.Work = this;
            timeblock.Subcon = this.Subcon;

            Random r = new Random();
            int rInt = r.Next(0, timeBlockLength*60); //for ints
            timeblock.ScreenShotCapturedSecond = rInt;
            return timeblock;
        }

        public bool SetScreenShot(MemoryStream screenShot)
        {
            
            if(currentTimeBlock != null)
            {
                Debug.WriteLine("screenShot : " + screenShot);
                currentTimeBlock.ScreenShot = screenShot;
                return true;
            }
            return false;
        }

        public void IncrementScreenShotTimer()
        {
            currentScreenShotTimer++;
        }

        public void ResetScreenShotTimer()
        {
            currentScreenShotTimer = 0;
        }

        public bool IsReadyForCapturingScreenShot()
        {
            Debug.WriteLine(currentScreenShotTimer +" = "+CurrentTimeBlock.ScreenShotCapturedSecond);
            return currentScreenShotTimer == CurrentTimeBlock.ScreenShotCapturedSecond;
        }

        public IProxyModel convert(dynamic jsonObject)
        {
            throw new NotImplementedException();
        }

        public dynamic ToJSON()
        {
            
            
            var properties = new Dictionary<string, object>()
            {
                {"subcon_id", this.Subcon.Id.ToString()},
                {"start", start.ToString("o")},
                {"end", end.ToString("o") }            
            };
            return properties;
            
        }
    }
}
