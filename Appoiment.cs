//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedicalClinic
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appoiment
    {
        public int idAppoiment { get; set; }
        public System.DateTime date { get; set; }
        public int idUser { get; set; }
        public int idDoctor { get; set; }
        public int idService { get; set; }
        public int startTime { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Service Service { get; set; }
        public virtual User User { get; set; }
    }
}
