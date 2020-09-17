using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace kutuphane.database.Models
{
   public class StudentDepartment
    {
        [Key]
        public int Id { get; set; }
        public int rlt_Department_Id { get; set; }

        public int rlt_Student_Id { get; set; }

        public DateTime RecordCreateDate { get; set; } = DateTime.Now;

        [ForeignKey("rlt_Student_Id")]
        public Student Student { get; set; }

        [ForeignKey("rlt_Department_Id")]
        public Department Department { get; set; }
    }
}
