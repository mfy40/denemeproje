using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kutuphane.ViewModels
{
    public class Book
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Kitap ismi giriniz !")]
        [DisplayName("Kitap İsmi")]
        public string Name { get; set; }

        [DisplayName("Seri No")]
        public string SerialNo { get; set; }

        [Required(ErrorMessage = "Kitap adeti giriniz !")]
        [DisplayName("Kitap Adeti")]
        public int StockNumber { get; set; }

        [DisplayName("Kod")]
        public string Kod { get; set; } // baskı no

        [Required(ErrorMessage = "Raf numarası giriniz !")]
        [DisplayName("Raf Numarası")]
        public string ShelfNo { get; set; } // kitaplıktaki raf numarası       

        [DisplayName("Basım Tarihi")]

        [DataType(DataType.Date)]

        public DateTime DatePrinting { get; set; } // basım tarihi

        [DisplayName("Yayın Tarihi")]
        [DataType(DataType.Date)]

        public DateTime DatePublish { get; set; } // yayın tarihi

        [Required(ErrorMessage = "Baskı numarası giriniz !")]
        [DisplayName("Baskı No")]
        public int EditionNo { get; set; } // baskı no


        [Required]
        [DisplayName("Yayın Evi")]
        public int PublisherId { get; set; }


        [Required]
        [DisplayName("Kitaplık No")]
        public int BookshelfId { get; set; }

        [Required]
        [DisplayName("Kategoriler")]
        public List<int> CategoriesIds { get; set; }

        [Required]
        [DisplayName("Yazarlar")]
        public List<int> WriterIds { get; set; }


        public List<Writer> Writers { get; set; }

        public List<Category> Categories { get; set; }



    }
}
