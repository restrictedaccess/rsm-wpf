using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Subcontractor
    {
        private int id;
        private Staff staff;
        private Client client;
        private float clientPrice;
        private float staffRate;
        private string staffEmail;
        private string jobDesignation;
        

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public Staff Staff
        {
            get
            {
                return staff;
            }

            set
            {
                staff = value;
            }
        }

        public Client Client
        {
            get
            {
                return client;
            }

            set
            {
                client = value;
            }
        }
        

        public string JobDesignation
        {
            get
            {
                return jobDesignation;
            }

            set
            {
                jobDesignation = value;
            }
        }

        public string StaffEmail
        {
            get
            {
                return staffEmail;
            }

            set
            {
                staffEmail = value;
            }
        }

        public float ClientPrice
        {
            get
            {
                return clientPrice;
            }

            set
            {
                clientPrice = value;
            }
        }

        public float StaffRate
        {
            get
            {
                return staffRate;
            }

            set
            {
                staffRate = value;
            }
        }

        public Staff AssignedStaff
        {
            get
            {
                return Staff;
            }

            set
            {
                Staff = value;
            }
        }

        public Client AssignedClient
        {
            get
            {
                return Client;
            }

            set
            {
                Client = value;
            }
        }

        public string Display
        {
            get
            {
                return id + " " + Client.FirstName + " " + Client.LastName + " " + jobDesignation;
            }
            
        }
    }
}
