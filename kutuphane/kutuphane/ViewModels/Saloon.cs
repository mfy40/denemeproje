using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kutuphane.ViewModels
{
    public class Saloon
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori ismi giriniz !")]
        [DisplayName("Kategori İsmi")]
        public string Name { get; set; }
    }
}
