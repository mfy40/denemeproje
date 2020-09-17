using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kutuphane.database.Models
{
    public class Book
    {

        [Key]
        public int Id { get; set; }
        public int rlt_Bookshelf_Id { get; set; }     
        public int rlt_Publisher_Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public string SerialNo { get; set; } // kitap serinosu

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 20 characters")]
        public int EditionNo { get; set; } // baskı no

        [MaxLength(10, ErrorMessage = "Length must be less then 10 characters")]
        public string? Kod { get; set; } // baskı no

        public string ShelfNo { get; set; } // kitaplıktaki raf numarası 
        public DateTime RecordCreateDate { get; set; } = DateTime.Now;

        public int StockNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePrinting { get; set; } // basım tarihi

        [DataType(DataType.Date)]
        public DateTime DatePublish { get; set; } // yayın tarihi

        [ForeignKey("rlt_Publisher_Id")]
        public Publisher Publisher { get; set; } // yayın evi 

        [ForeignKey("rlt_Bookshelf_Id")]
        public Bookshelf Bookshelf { get; set; } // kitaplık 
    
        public ICollection<BookCategory> BookCategories { get; set; }
        public ICollection<BookWriter> BookWriters { get; set; }




    }
}
