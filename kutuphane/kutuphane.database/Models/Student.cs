using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace kutuphane.database.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

    


        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public string Surname { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "Length must be less then 20 characters")]

        public string CitizenshipNo { get; set; }


        [Required]
        [MaxLength(11, ErrorMessage = "Length must be less then 11 characters")]

        public string Phone { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Length must be less then 100 characters")]
        public string Adress { get; set; }

        public int StudentNo { get; set; }

        public DateTime RecordCreateDate { get; set; } = DateTime.Now;
        
        public ICollection<StudentDepartment> StudentDepartments { get; set; }

    }
}
