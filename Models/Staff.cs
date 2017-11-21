using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Staff:Person
    {
        private List<Subcontractor> contracts = new List<Subcontractor>();
        private Subcontractor selectedContract;

        public List<Subcontractor> Contracts
        {
            get
            {
                return contracts;
            }
        }

        public Subcontractor SelectedContract
        {
            get
            {
                return selectedContract;
            }

            set
            {
                selectedContract = value;
            }
        }

        public override IProxyModel convert(dynamic jsonObject)
        {
            this.FirstName = jsonObject.fname;
            this.LastName = jsonObject.lname;

            
            ModelBuilder.GetInstance(ModelBuilder.CLIENT, jsonObject);
            return this;   
        }

        public override dynamic ToJSON()
        {
            throw new NotImplementedException();
        }
    }
}
