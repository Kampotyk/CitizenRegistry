using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citizens
{
    public class CitizenRegistry : ICitizenRegistry
    {
        public void Register(ICitizen citizen)
        {
            throw new NotImplementedException();
        }

        public ICitizen this[string id]
        {
            get { throw new NotImplementedException(); }
        }

        public string Stats()
        {
            throw new NotImplementedException();
        }
    }
}
