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
    using System.ComponentModel.DataAnnotations;

    public partial class RequestHelp
    {
        [Display(Name = "Номер заявки")]
        public int RequestID { get; set; }
        [Display(Name = "Сотрудник")]
        [Required(ErrorMessage = "Выберите сотрудника, от которого поступает заявка")]
        public string PassportID { get; set; }
        [Display(Name = "Описание проблемы")]
        [Required(ErrorMessage = "Кратко опишите проблему")]
        [MinLength(2, ErrorMessage = "Минимальная длина описание - 2 символа")]
        [MaxLength(255, ErrorMessage = "Максимальная длина описания - 255 символов")]
        public string Description { get; set; }
        [Display(Name = "Статус заявки")]
        [Required(ErrorMessage = "Выберите статус заявки")]
        public int StatusID { get; set; }
    
        public virtual CodifierRequestHelp CodifierRequestHelp { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
