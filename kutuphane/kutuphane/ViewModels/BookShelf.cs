using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kutuphane.ViewModels
{
    public class BookShelf
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Salon seciniz !")]
        [DisplayName("Salon")]
        public int SelectSalonId { get; set; }

        [Required(ErrorMessage = "Kitaplık ismi giriniz !")]
        [DisplayName("Kitaplık İsmi")]
        public string Name { get; set; }
       
        public Saloon Saloon { get; set; }  

    }
}
