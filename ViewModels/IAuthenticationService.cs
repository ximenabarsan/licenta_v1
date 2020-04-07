using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

    namespace MedicalClinic
    {
        public interface IAuthenticationService
        {
            User AuthenticateUser(string email, string password);
        }

        public class AuthenticationService : IAuthenticationService
        {
            private class InternalUserData
            {
                public InternalUserData(string username, string email, string hashedPassword, string[] roles)
                {
                    Username = username;
                    Email = email;
                    HashedPassword = hashedPassword;
                    Roles = roles;
                }
                public string Username
                {
                    get;
                    private set;
                }

                public string Email
                {
                    get;
                    private set;
                }

                public string HashedPassword
                {
                    get;
                    private set;
                }

                public string[] Roles
                {
                    get;
                    private set;
                }
            }

         

        public User AuthenticateUser(string email, string password)
        {
            
            using (var context = new MedicalDBEntities())
            {
                var query = context.Users
                        .Where(s => s.email == email&&s.password==password)
                        .FirstOrDefault<User>();

                User user = query;
                if (user == null)
                    throw new UnauthorizedAccessException("Contul dumneavoastra nu a fost gasit, va rugam sa reincercati.");

                else
                {
                    return user;
                }
            }
        }

        }

        
        
    }

