using RSM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Screna;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Remote_Staff_Monitoring_App
{
    class CaptureScreenshotJob : Job
    {
        public override void DoJob()
        {

            try
            {
                if (RemoteStaffSystem.GetInstance().CurrentWork.IsReadyForCapturingScreenShot())
                {
                    //ScreenshotCapture captureModule = new ScreenshotCapture();
                    //Image img = captureModule.CaptureScreen();
                    //MemoryStream screenShot = new MemoryStream();
                    //img.Save(screenShot, ImageFormat.Jpeg);
                    //screenShot.Position = 0;
                    Bitmap imageScrena = Screna.ScreenShot.Capture(true, true);
                    MemoryStream screenShot = new MemoryStream();
                    imageScrena.Save(screenShot, ImageFormat.Jpeg);
                    BitmapImage image;
                    using (Stream stream = screenShot)
                    {
                        image = new BitmapImage();
                        stream.Position = 0;
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = stream;
                        image.EndInit();
                        image.Freeze();
                    }



                    RemoteStaffSystem.GetInstance().UpdateImage(image);
                    //RemoteStaffSystem.GetInstance().StartWorkForm.updateNotification();

                    imageScrena = Screna.ScreenShot.Capture(true, true);
                    screenShot = new MemoryStream();
                    imageScrena.Save(screenShot, ImageFormat.Jpeg);

                    RemoteStaffSystem.GetInstance().CurrentWork.SetScreenShot(screenShot);
                    RemoteStaffSystem.GetInstance().CurrentWork.IncrementScreenShotTimer();
                    //Console.WriteLine("Capturing ScreenShot");


                    CaptureActiveAppsJob activeAppsJob = new CaptureActiveAppsJob();

                    Thread thread = null;
                    try
                    {
                       
                        thread = new Thread(new ThreadStart(activeAppsJob.DoJob));
                        // start thread executing the job
                        thread.Start();
                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    RemoteStaffSystem.GetInstance().CurrentWork.IncrementScreenShotTimer();
                    //Console.WriteLine("Not ready for capturing screen shot");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            
            
        }

        public override string GetName()
        {
            return "Capture Screenshot Job";
        }

        public override int GetRepetitionIntervalTime()
        {
            
            return 1000;
        }

        public override bool IsRepeatable()
        {
            return true;
        }
    }
}
