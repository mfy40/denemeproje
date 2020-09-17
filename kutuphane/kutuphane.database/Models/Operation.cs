using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace kutuphane.database.Models
{
    public class Operation
    {
        [Key]
        public int Id { get; set; }
        public int rlt_Student_Id { get; set; }
        public int rlt_Book_Id { get; set; }

        public DateTime RecordCreateDate { get; set; } = DateTime.Now;
        public DateTime GivenDate { get; set; }
        public DateTime? TakenDate { get; set; }
        public DateTime DateOfTaken { get; set; }

        [ForeignKey("rlt_Student_Id")] //relation 
        public Student Student { get; set; }

        [ForeignKey("rlt_Book_Id")]
        public Book Book { get; set; }




    }
}
