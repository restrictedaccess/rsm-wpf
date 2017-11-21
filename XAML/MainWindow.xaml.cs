using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Remote_Staff_Modules;
using Couchbase.Lite;
using Couchbase.Lite.Util;
using Couchbase.Lite.Auth;
using RSM;
using Models;

namespace XAML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string VERSION = "1.0";

        public MainWindow()
        {
            InitializeComponent();


        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)        
        {
            VersionData versionData = new VersionData();
            versionData.CurrentVersion = VERSION;

            BaseModule module = ModuleFactory.Get(ModuleFactory.Version);
            try
            {
                Task<bool> processResult = module.Process(versionData);
                bool result = await processResult;                
                if (result == false)
                {
                    Console.WriteLine("Version Mismatch");
                    loginButton.IsEnabled = false;
                    MessageBox.Show("Version Mismatch");
                }
                else
                {
                    loginButton.IsEnabled = true;
                    Console.WriteLine("Updated Version : " +VERSION);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex.Message);                
            }


        }
    
        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginData loginData = new LoginData();
            loginData.EmailAddress = Email.Text;
            loginData.Password = UserPassword.Password;

            loginButton.IsEnabled = false;            
            loginButton.Content = "signing in...";


            BaseModule module = ModuleFactory.Get(ModuleFactory.Login);
            var result = await module.Process(loginData);
            if (result)
            {
                CouchBaseReplicatorStaff replicatorStaff = new CouchBaseReplicatorStaff();
                replicatorStaff.InitializeReplication((LoginModule)module);

                RemoteStaffSystem.GetInstance().LoginModule = (LoginModule)module;
                RemoteStaffSystem.GetInstance().ShowStartWork();

                Staff staff = RemoteStaffSystem.GetInstance().LoginModule.LoggedInStaff;
                replicatorStaff.syncContract(staff);

                CouchBaseReplicatorWork replicatorWork = new CouchBaseReplicatorWork();
                replicatorWork.InitializeReplication((LoginModule)module);

                CouchBaseReplicatorTimeBlock replicatorTimeBlock = new CouchBaseReplicatorTimeBlock();
                replicatorTimeBlock.InitializeReplication((LoginModule)module);

                this.Close();
            }
            else
            {
                dynamic processResult = module.GetResult();

                MessageBox.Show(processResult["msg"], "Error!");
                loginButton.IsEnabled = true;
                loginButton.Content = "Login";
            }

        }

        private void ShowHidePasswordPlaceHolder()
        {
            if (UserPassword.Password == "")
            {
                PasswordPlaceHolder.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordPlaceHolder.Visibility = Visibility.Collapsed;
            }
        }

        private void UserPassword_TextInput(object sender, TextCompositionEventArgs e)
        {
            ShowHidePasswordPlaceHolder(); 
        }

        private void UserPassword_KeyDown(object sender, KeyEventArgs e)
        {
            ShowHidePasswordPlaceHolder();
        }

        private void PasswordPlaceHolder_LostFocus(object sender, RoutedEventArgs e)
        {
            ShowHidePasswordPlaceHolder();
        }

        
    }
}
