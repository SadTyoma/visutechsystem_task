using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task.Models
{
    public class User
    {
        public enum SexOfPerson
        {
            Male,
            Female
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ID { get; set; }
        public string Age { get; set; }
        public SexOfPerson Sexofperson { get; set; }
        public User(string firstname, string lastname, string id, string age, SexOfPerson sexofperson)
        {
            FirstName = firstname;
            LastName = lastname;
            ID = id;
            Age = age;
            Sexofperson = sexofperson;
        }
    }
}

