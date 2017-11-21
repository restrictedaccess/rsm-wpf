using System;
using System.Threading.Tasks;
using System.Diagnostics;
using jParser;
using Models;

namespace Remote_Staff_Modules
{
    public class LoginModule : BaseModule
    {
        private Staff loggedInStaff;

        public Staff LoggedInStaff
        {
            get
            {
                return loggedInStaff;
            }

            set
            {
                loggedInStaff = value;
            }
        }

        public override Task<bool> Process()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Process(ICouchSave couchSave)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Process(IProcessData processData)
        {
            if (!processData.Validate())
            {
                Debug.WriteLine("Error");
                Debug.WriteLine(processData.GetErrors().Count);
                foreach (string error in processData.GetErrors())
                {
                    Debug.WriteLine(error);
                }

                return false;                
            }

            LoginData loginData = (LoginData)processData;
            Task<string> taskResult = Post(loginData.GetFormData(), "/rsm/signin/");
            string result = await taskResult;
            dynamic json = jParser.Parser.Parse(result);
            bool success = json["success"];
            Debug.WriteLine("LoginModule : " + success);
            
            
            if (success)
            {
                Staff staff = new Staff();
                staff.EmailAddress = json["result"]["email"];
                staff.FirstName = json["result"]["fname"];
                staff.LastName = json["result"]["lname"];
                staff.Id = json["result"]["userid"];
                staff.Password = loginData.Password;
                LoggedInStaff = staff;               
            }


            

            RESULT = json;
            return success;

        }

        public override Task<bool> Process(IProcessData processData, ICouchSave couchSave)
        {
            throw new NotImplementedException();
        }
    }
}
