using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class ActiveSubstances_SideEffects
    {
        private string sideEffects;
        public string SideEffects
        {
            get { return sideEffects; }
            set { sideEffects = value; }
        }

        private int activeSubstancesID;
        public int ActiveSubstancesID
        {
            get { return activeSubstancesID; }
            set { activeSubstancesID = value; }
        }

        // Additional properties or methods as needed
    }
}
