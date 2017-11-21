using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using RSM;

namespace Remote_Staff_Monitoring_App
{
    class CaptureActiveAppsJob
    {
        public void DoJob()
        {
            Process[] processes = Process.GetProcesses();
            //Console.WriteLine("Capture Activity Apps");
            foreach (Process process in processes)
            {
                //Console.WriteLine(process.ToString());
                RemoteStaffSystem.GetInstance().CurrentWork.CurrentTimeBlock.Processes.Add(process.ToString());
            }
            
        }

       
    }
}
