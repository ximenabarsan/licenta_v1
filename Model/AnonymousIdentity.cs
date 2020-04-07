using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalClinic
{
   public class AnonymousIdentity:CustomIdentity
    {
        /// <summary>
        /// clasa pentru userii neautentificati
        /// </summary>
        public AnonymousIdentity():base(string.Empty,new int(), new string[] {"NoRights"},string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        {

        }
    }
}
