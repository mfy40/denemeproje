using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace kutuphane.database.Models
{
   public class Faculty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Length must be less then 30 characters")]
        public string Name { get; set; }
        public DateTime RecordCreateDate { get; set; } = DateTime.Now;

        public ICollection<Department> Departments { get; set; }

    }
}
