using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnWoodStore.Model.Database
{
    class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Emal { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public int UNP { get; set; }

        public List<UserType> UserTypes { get; set; }
        public Cart Cart { get; set; }
    }
}
