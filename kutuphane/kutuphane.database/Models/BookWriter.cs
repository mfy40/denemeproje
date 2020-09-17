using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace kutuphane.database.Models
{
   public class BookWriter
    {
        [Key]
        public int Id { get; set; }

        
        public int rlt_Book_Id { get; set; }

        public int rlt_Writer_Id { get; set; }
        public DateTime RecordCreateDate { get; set; } = DateTime.Now;

        [ForeignKey("rlt_Book_Id")]
        public Book Book { get; set; }


        [ForeignKey("rlt_Writer_Id")]
        public Writer Writer { get; set; }

    }
}
