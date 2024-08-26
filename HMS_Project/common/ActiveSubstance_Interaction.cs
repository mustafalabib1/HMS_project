using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class ActiveSubstance_Interaction
    {
        private int activeSubstanceID1;
        public int ActiveSubstanceID1
        {
            get { return activeSubstanceID1; }
            set { activeSubstanceID1 = value; }
        }

        private int activeSubstanceID2;
        public int ActiveSubstanceID2
        {
            get { return activeSubstanceID2; }
            set { activeSubstanceID2 = value; }
        }

        private string interaction;
        public string Interaction
        {
            get { return interaction; }
            set { interaction = value; }
        }

        // Additional properties or methods as needed
    }
}
