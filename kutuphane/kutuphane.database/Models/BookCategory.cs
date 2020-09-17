using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace kutuphane.database.Models
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }

        public int rtl_Category_Id { get; set; }

       
        public int rtl_Book_Id { get; set; }

        public DateTime RecordCreateDate { get; set; } = DateTime.Now;

        [ForeignKey("rtl_Book_Id")]
        public Book Book { get; set; }

        [ForeignKey("rtl_Category_Id")]
        public Category Category { get; set; }  

    }
}
