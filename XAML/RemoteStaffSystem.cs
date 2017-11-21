using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Remote_Staff_Modules;
using Models;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Captura;

namespace RSM
{
    class RemoteStaffSystem
    {
        private static RemoteStaffSystem instance;
        private MainViewModel captura = new MainViewModel();


        private LoginModule loginModule;
        private StartWorkWindow startWork;
        
        private BaseModule startWorkModule;
        

        private Work currentWork;
        private bool notWorking = true;
        private int MovementPerHour = 4000;

        private int TimeBlockLengthInMinute = 3;


        public int GetTimeBlocksPerHour()
        {
            return 60 / TimeBlockLengthInMinute;
        }
        public int GetMovementPerTimeBlock()
        {
            return MovementPerHour / GetTimeBlocksPerHour();
        }

        public LoginModule LoginModule
        {
            get
            {
                return loginModule;
            }

            set
            {
                loginModule = value;
            }
        }


        public StartWorkWindow StartWorkwindow
        {
            get
            {
                return startWork;
            }

            set
            {
                startWork = value;
            }
        }

        public void UpdateImage(BitmapImage image)
        {

            StartWorkwindow.Dispatcher.Invoke(new Action(() => {
                StartWorkwindow.screenshotImage.Source = image;
            }));
           
        }

        public void StartRecording()
        {

            StartWorkwindow.Dispatcher.Invoke(new Action(() => {
               Captura.StartRecording();
            }));

        }
        public void StopRecording()
        {

            StartWorkwindow.Dispatcher.Invoke(new Action(() => {
                Captura.StopRecording();
            }));

        }




        public void UpdateStatus(String status)
        {
            try
            {
                StartWorkwindow.Dispatcher.Invoke(new Action(() => {
                    StartWorkwindow.workingStatus.Text = status;
                }));
            }
            catch (Exception e)
            {

            }

        }

        public BaseModule StartWorkModule
        {
            get
            {
                return startWorkModule;
            }

            set
            {
                startWorkModule = value;
            }
        }

        public Work CurrentWork
        {
            get
            {
                return currentWork;
            }

            set
            {
                currentWork = value;
            }
        }

        public bool NotWorking
        {
            get
            {
                return notWorking;
            }

            set
            {
                notWorking = value;
            }
        }

        public int MovementPerHour1
        {
            get
            {
                return MovementPerHour;
            }

            set
            {
                MovementPerHour = value;
            }
        }

        public MainViewModel Captura
        {
            get
            {
                return captura;
            }

            set
            {
                captura = value;
            }
        }

        public async Task<bool> StartWork()
        {
            var selectedContract = loginModule.LoggedInStaff.SelectedContract;
            Console.WriteLine(selectedContract);
            if (selectedContract != null)
            {

                string client_name = selectedContract.Client.Name;
                if (MessageBox.Show("Are you sure you want to start work with " + client_name + "?", "Start Work", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    StartWorkData startWork = new StartWorkData();
                    startWork.Subcon = selectedContract;

                    Task<bool> task = StartWorkModule.Process(startWork, new CouchbaseLiteSaveWork());
                    var result = await task;
                    if (result)
                    {
                        RemoteStaffSystem.GetInstance().UpdateStatus("Working");
                        CurrentWork = StartWorkModule.GetResult();
                        CurrentWork.TimeBlockLength = TimeBlockLengthInMinute;
                        var manager = new JobManager();
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
                        manager.ExecuteAllJobs();

                        //BaseModule module = ModuleFactory.Get(ModuleFactory.Login);
                        //CouchBaseReplicatorTimeBlock replicatorTimeBlock = new CouchBaseReplicatorTimeBlock();
                        //replicatorTimeBlock.InitializeReplication((LoginModule)module);


                    }


                }

                return true;
            }
            else
            {
                MessageBox.Show("Please select a client");
                return false;
            }

        }

        public static RemoteStaffSystem GetInstance()
        {
            if (instance==null)
            {
                instance = new RemoteStaffSystem();
            }
            return instance;
        }


        public void ShowStartWork()
        {
            startWorkModule = ModuleFactory.Get(ModuleFactory.StartWork);
            this.StartWorkwindow = new StartWorkWindow();
            StartWorkwindow.Show();
        }
    }
}
