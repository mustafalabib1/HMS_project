using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    [ComplexType]
    public class FullName
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string? MiddleName { get; set; }
        [MaxLength(50) ]
        public string LastName { get; set; }

        public FullName(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public FullName(string firstName, string lastName):this(firstName,null,lastName)
        {
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(MiddleName))
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
            else
            {
                return $"{FirstName} {LastName}";
            }
        }
    }

}
