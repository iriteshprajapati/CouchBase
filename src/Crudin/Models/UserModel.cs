using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crudin.Models
{
    public class UserModel
    {

        public string id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Password { get; set; }
        public string Confirmpassword { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
    }
}
