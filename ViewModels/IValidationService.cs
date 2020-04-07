using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalClinic.ViewModels
{
    
   public class IValidationService
    {
        public bool ValidateName(string nume, out ICollection<string> validationErrorsName) {
            validationErrorsName = new List<string>();
            if (nume==null)
                validationErrorsName.Add("Casuta trebuie completatata cu datele dumnevoastra");

            return true;
        }
        public bool ValidateEmailRegister(string email, out ICollection<string> validationErrorsEmailRegister) {
            using (var context = new MedicalDBEntities())
            {
                validationErrorsEmailRegister = new List<string>();
                int count = 0;
                var query = context.Users.Where(s => s.email == email);
                var result = query;
                if (result != null)
                {
                    validationErrorsEmailRegister.Add("Acest cont exista deja");
                    count++;
                }

                return count==0;
            }
        }
        public bool ValidateEmailAuthentication(string email, out ICollection<string> validationErrorsEmailAuthentication)
        {
            validationErrorsEmailAuthentication = new List<String>();
            int count = 0;
               
                
                if (!email.Contains("@")) 
                {
                    validationErrorsEmailAuthentication.Add("Contul dumneavoastra trebuie sa\nfie de tipul ceva@yahoo.com");
                    count++;
                }

                return count == 0;
            
        }

        private bool ValidatePassword(string password, out ICollection<string> validationErrors){
            validationErrors = new List<string>();
            int count = 0;
            int nUpper = 0;
            int nNumber = 0;
            
           foreach (char letter in password) {
                if (Char.IsUpper(letter)) 
                {
                    nUpper++; 
                }
                if (Char.IsDigit(letter)) {
                    nNumber++;
                }
            
            }

            if (nUpper == 0 || nNumber == 0||password.Length<8)
            {
                validationErrors.Add("Parola trebuie sa contina cel putin 8 caracter,\n printre care o cifra si o majuscula.");
                count++;
            }
    
            return count == 0;

           
      }

    }
}
