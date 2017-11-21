using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Client:Person
    {
        private List<Subcontractor> contracts = new List<Subcontractor>();

        public List<Subcontractor> Contracts
        {
            get
            {
                return contracts;
            }
        }

        public override IProxyModel convert(dynamic jsonObject)
        {
            throw new NotImplementedException();
        }

        public override dynamic ToJSON()
        {
            throw new NotImplementedException();
        }
    }
}
