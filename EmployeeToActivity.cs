//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PracticeActivityTraking
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeToActivity
    {
        public int ConnectID { get; set; }
        public string PassportID { get; set; }
        public int ActivityID { get; set; }
    
        public virtual Activity Activity { get; set; }
        public virtual Employee Employee { get; set; }
    }
}