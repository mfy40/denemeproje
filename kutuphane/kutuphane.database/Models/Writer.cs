using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace kutuphane.database.Models
{
    public class Writer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }    
        public DateTime RecordCreateDate { get; set; } = DateTime.Now;

        public ICollection<BookWriter> BookWriters { get; set; }

    }
}
