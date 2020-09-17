using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace kutuphane.database.Models
{
   public class Bookshelf
    {
        [Key]
        public int Id { get; set; }

        public int rlt_Saloon_Id { get; set; }

        public string Name { get; set; }
        public DateTime RecordCreateDate { get; set; } = DateTime.Now;

        [ForeignKey("rlt_Saloon_Id")]
        public Saloon Saloon { get; set; } // bu kitaplık hangi salonda yer alacak 
    }
}
