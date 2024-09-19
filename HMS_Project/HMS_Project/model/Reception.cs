using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    internal class Reception
    {
        
        private int _receptionID;
        public int ReceptionID
        {
            get { return _receptionID; }
            set { _receptionID = value; }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Phone cannot be null or empty");
                }
                if (value.Length > 20)
                {
                    throw new ArgumentException("Phone number cannot exceed 20 characters");
                }
                _phone = value;
            }
        }
    }
}
