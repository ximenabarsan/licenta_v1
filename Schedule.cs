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
    
    public partial class Schedule
    {
        public int idSchedule { get; set; }
        public int StartSchedule { get; set; }
        public int FinishSchedule { get; set; }
        public int idDoctor { get; set; }
        public int idDay { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Week Week { get; set; }
    }
}
