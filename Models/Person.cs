using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public abstract class Person: IProxyModel
    {
        private int id;
        private string firstName;
        private string lastName;
        private string emailAddress;
        private string password;

       
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string Greetings
        {
            get
            {
                return "Hi, " + FirstName + " !";
            }
        }


        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }

            set
            {
                emailAddress = value;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

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


        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public abstract IProxyModel convert(dynamic jsonObject);
        public abstract dynamic ToJSON();
    }
}
