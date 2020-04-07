using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MedicalClinic
{
   public class CustomIdentity : IIdentity
    {   int IdUser;
        string Telephone;
        string Surname;
        string CNP;
        string Password;

        public CustomIdentity(string name, int idUser, string[] roles, string email,string telephone, string surname,string cnp,string password)
        {
            Name = name;
            Email = email;
            Roles = roles;
            IdUser = idUser;
            Telephone = telephone;
            Surname = surname;
            CNP = cnp;
            Password = password;


        }


        public string Name { get; private set; }
        public string Email { get; private set; }
        public string[] Roles { get; private set; }

        public string AuthenticationType { get { return " Custom authentication"; }}
        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }


    }
}
