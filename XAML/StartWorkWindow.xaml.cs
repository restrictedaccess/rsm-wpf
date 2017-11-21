using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Models;
using System.ComponentModel;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
//using Remote_Staff_Modules;

namespace RSM
{
    /// <summary>
    /// Interaction logic for StartWorkWindow.xaml
    /// </summary>
    public partial class StartWorkWindow : Window
    {
        private Staff loggedInStaff;
        private IKeyboardMouseEvents mEvents;


        public StartWorkWindow()
        {
            InitializeComponent();
            loggedInStaff = RemoteStaffSystem.GetInstance().LoginModule.LoggedInStaff;
            this.DataContext = this;
            StaffName.DataContext = loggedInStaff;
            clientsBox.DataContext = loggedInStaff;
            RemoteStaffSystem.GetInstance().UpdateStatus("Not Working");
            
        }

        private async void startWorkButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(RemoteStaffSystem.GetInstance().LoginModule.LoggedInStaff.SelectedContract.Display);
            Task<bool> result = RemoteStaffSystem.GetInstance().StartWork();            
            var startWork = await result;
            SubscribeGlobal();

            //BaseModule module = ModuleFactory.Get(ModuleFactory.Login);
            //CouchBaseReplicatorTimeBlock replicatorTimeBlock = new CouchBaseReplicatorTimeBlock();
            //replicatorTimeBlock.InitializeReplication((LoginModule)module);

        }


        private void SubscribeGlobal()
        {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
        }

        private void Unsubscribe()
        {
            if (mEvents == null)
            {
                return;
            }

            //KeyBoard
            mEvents.KeyDown -= StartWorkForm_KeyDown;
            mEvents.KeyUp -= StartWorkForm_KeyUp;
            mEvents.KeyPress -= StartWorkForm_KeyPress;

            //Mouse
            //Mouse
            mEvents.MouseClick -= StartWorkForm_MouseClick;
            mEvents.MouseMove -= StartWorkForm_MouseMove;
            mEvents.MouseUp -= StartWorkForm_MouseUp;
            mEvents.MouseDoubleClick -= StartWorkForm_MouseDoubleClick;
            mEvents.MouseDragStarted -= OnMouseDragStarted;
            mEvents.MouseDragFinished -= OnMouseDragFinished;

        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            mEvents = events;

            //Keyboard
            mEvents.KeyDown += StartWorkForm_KeyDown;
            mEvents.KeyUp += StartWorkForm_KeyUp;
            mEvents.KeyPress += StartWorkForm_KeyPress;

            //Mouse
            mEvents.MouseClick += StartWorkForm_MouseClick;
            mEvents.MouseDown += StartWorkForm_MouseDown;
            mEvents.MouseMove += StartWorkForm_MouseMove;
            mEvents.MouseUp += StartWorkForm_MouseUp;
            mEvents.MouseDoubleClick += StartWorkForm_MouseDoubleClick;
            mEvents.MouseDragStarted += OnMouseDragStarted;
            mEvents.MouseDragFinished += OnMouseDragFinished;
        }


        private void StartWorkForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // Console.WriteLine("KeyDown : " + e.KeyCode);
        }

        private void StartWorkForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //Console.WriteLine("KeyUp : " + e.KeyCode);
        }

        private void StartWorkForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Console.WriteLine("KeyPress : " + e.KeyChar);
            Models.Keyboard keyboard = new Models.Keyboard();
            keyboard.ActivityEvent = "KeyPress";
            keyboard.DateActivity = new DateTime();
            keyboard.Keys = e.KeyChar.ToString();
            try
            {
                RemoteStaffSystem.GetInstance().CurrentWork.CurrentTimeBlock.Activities.Add(keyboard);
                RemoteStaffSystem.GetInstance().CurrentWork.CurrentTimeBlock.LastActivityDateTime = DateTime.Today;
                Console.WriteLine("Activity Count: " + RemoteStaffSystem.GetInstance().CurrentWork.CurrentTimeBlock.Activities.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }

        private void StartWorkForm_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Console.WriteLine("MouseClick : " + e.Button);

            Models.Mouse mouse = new Models.Mouse();
            mouse.ActivityEvent = "MouseClick";
            mouse.MouseButton = e.Button.ToString();
            mouse.DateActivity = new DateTime();
            try
            {
                RemoteStaffSystem.GetInstance().CurrentWork.CurrentTimeBlock.Activities.Add(mouse);
                RemoteStaffSystem.GetInstance().CurrentWork.CurrentTimeBlock.LastActivityDateTime = DateTime.Today;
                Console.WriteLine("Activity Count: " + RemoteStaffSystem.GetInstance().CurrentWork.CurrentTimeBlock.Activities.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }

        private void StartWorkForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Console.WriteLine("MouseDown : " + e.Button);
        }

        private void StartWorkForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Console.WriteLine("MouseMove : " + e.Button);
        }

        private void StartWorkForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Console.WriteLine("MouseUp : " + e.Button);
        }

        private void StartWorkForm_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Console.WriteLine("MouseDoubleClick : " + e.Button);
        }

        private void OnMouseDragStarted(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Console.WriteLine("OnMouseDragStarted : " + e.Button);
        }


        private void OnMouseDragFinished(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Console.WriteLine("OnMouseDragFinished : " + e.Button);
        }

        private void _this_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void _this_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
