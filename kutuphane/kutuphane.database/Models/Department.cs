using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace kutuphane.database.Models
{
   public class Department
    {
        [Key]
        public int Id { get; set; }

        
        public int rlt_Faculty_Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Length must be less then 30 characters")]
        public string Name { get; set; }
        public DateTime RecordCreateDate { get; set; } = DateTime.Now;
        public DateTime RecruitmentDate { get; set; } // üniversite giriş  tarihi 
        public DateTime? TerminationDate { get; set; }  // üniversiteden çıkış tarihi

        [ForeignKey("rlt_Faculty_Id")]
        public Faculty Faculty { get; set; }
        public ICollection<StudentDepartment> StudentDepartments { get; set; }


    }
}
