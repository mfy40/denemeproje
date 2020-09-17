using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace kutuphane.database.Models
{
   public class Pricing
    {

        [Key]
        public int Id { get; set; }

        public int rlt_Student_Id { get; set; }

        public DateTime RecordCreateDate { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }

        [ForeignKey("rlt_Student_Id")]
        public Student Student { get; set; }
        public ICollection<Book> Books { get; set; }

    }
}
