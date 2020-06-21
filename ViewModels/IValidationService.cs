using MedicalClinic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MedicalClinic.ViewModels
{
    
   public class IValidationService
    { 
        
          public bool ValidateEmailRegister(string email, out ICollection<string> validationErrorsEmailRegister) {
              using (var context = new MedicalDBEntities())
              {
                  validationErrorsEmailRegister = new List<string>();
                int count = 0;
            
               if(!Regex.IsMatch(email,
                        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                        RegexOptions.IgnoreCase))
                    { 
               
                    validationErrorsEmailRegister.Add("Introduceti o adresa valida de email");
                    count++;
                }

                return count == 0;
            
        }
       }
          public bool ValidateEmailAuthentication(string email, out ICollection<string> validationErrorsEmailAuthentication)
          {
              validationErrorsEmailAuthentication = new List<String>();
              int count = 0;


            if (!Regex.IsMatch(email,
                 @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                 RegexOptions.IgnoreCase))
            {
                      validationErrorsEmailAuthentication.Add("Introduceti o adresa valida de email");
                      count++;
                  }

                  return count == 0;

          }

          public bool ValidatePasswordRegistration(string password, out ICollection<string> validationErrors){
          
           
            validationErrors = new List<string>();
              int count = 0;
              int nUpper = 0;
              int nNumber = 0;

             foreach (char letter in password ) {
                  if (Char.IsUpper(letter)) 
                  {
                      nUpper++; 
                  }
                  if (Char.IsDigit(letter)) {
                      nNumber++;
                  }
               

              }

              if (nUpper == 0 || nNumber == 0|| password.Length<8)
              {
                  validationErrors.Add("Parola trebuie sa contina cel putin 8 caracter,\n printre care o cifra si o majuscula.");
                  count++;
              }

              return count == 0;


        }
        public bool ValidateNameRegister(string name, out ICollection<string> validationErrorsNameRegister) {
            validationErrorsNameRegister = new List<String>();
            int count = 0;


            if (!Regex.IsMatch(name, @"(?i)^[a-z]+$"))
            {
                validationErrorsNameRegister.Add("Numele poate sa contina numai litere");
                count++;
            }

            return count == 0;


        }
        public bool ValidateTelephoneRegister(string telephone, out ICollection<string> validationErrorsTelephoneRegister)
        {
            validationErrorsTelephoneRegister = new List<String>();
            int count = 0;


            if (!Regex.IsMatch(telephone, @"^[0-9]+$") || telephone.Length!=10 )
            {
                validationErrorsTelephoneRegister.Add("Numarul de telefon poate sa contina numai 10 cifre");
                count++;
            }

            return count == 0;


        }
        public bool ValidateCnpRegister(string cnp, out ICollection<string> validationErrorsCnpRegister)
        {
            validationErrorsCnpRegister = new List<String>();
            int count = 0;


            if (!Regex.IsMatch(cnp, @"^[0-9]+$") || cnp.Length != 13)
            {
                validationErrorsCnpRegister.Add("Cnp-ul trebuie sa contina exact 13 cifre");
                count++;
            }

            return count == 0;


        }


    }
}

