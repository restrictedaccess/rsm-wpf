using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Remote_Staff_Modules;
using Captura;

namespace RSM
{
    public class CreateTimeBlockJob : Job
    {



        public override void DoJob()
        {
            RemoteStaffSystem.GetInstance().CurrentWork.CreateTimeBlock();
            TimeBlock previousTimeBlock = RemoteStaffSystem.GetInstance().CurrentWork.PreviousTimeBlock;
            
            RemoteStaffSystem.GetInstance().NotWorking = false;
            RemoteStaffSystem.GetInstance().CurrentWork.ResetScreenShotTimer();
            

            try
            {

                Console.WriteLine("Activities vs. Ideal Movement Per Timeblock: " + previousTimeBlock.Activities.Count + " : " + RemoteStaffSystem.GetInstance().GetMovementPerTimeBlock());
                if (previousTimeBlock.Activities.Count < RemoteStaffSystem.GetInstance().GetMovementPerTimeBlock())
                {
                    Console.WriteLine("The staff logged in is idle!");
                    RemoteStaffSystem.GetInstance().CurrentWork.PreviousTimeBlock.Status = "idle";
                    RemoteStaffSystem.GetInstance().UpdateStatus("Idle");
                }
                else
                {
                    Console.WriteLine("The staff logged in is active!");
                    RemoteStaffSystem.GetInstance().CurrentWork.PreviousTimeBlock.Status = "active";
                    RemoteStaffSystem.GetInstance().UpdateStatus("Working");
                }


                //Save to Couchbase
                if (previousTimeBlock != null)
                {
                    BaseModule timeBlockModule = ModuleFactory.Get(ModuleFactory.Timeblock);
                    TimeBlockData timeBlockData = new TimeBlockData();
                    timeBlockData.TimeBlock = previousTimeBlock;
                    timeBlockModule.Process(timeBlockData, new CouchbaseLiteSaveTimeBlocks());
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public override string GetName()
        {
            return "Create Time Block";
        }

        public override int GetRepetitionIntervalTime()
        {
            //Withing 3 Mins
            //Every 1000 = 1 sec
            return 180000; // 10 sec
        }

        public override bool IsRepeatable()
        {
            return true;
        }
    }
}
