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

    public partial class CodifierDepartment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CodifierDepartment()
        {
            this.Posts = new HashSet<Post>();
        }

        [Display(Name = "Код подразделения")]
        public int DepartmentID { get; set; }
        [Display(Name = "Подразделение")]
        [MinLength(2, ErrorMessage = "Минимальная длина наименования подразделения - 2 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина наименования подразделения - 50 символов")]
        [Required(ErrorMessage = "Введите название подразделения")]
        public string DepartmentName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
