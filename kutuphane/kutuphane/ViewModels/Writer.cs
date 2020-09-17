using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kutuphane.ViewModels
{
    public class Writer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Yazar ismi giriniz !")]
        [DisplayName("İsim")]
        public string Name { get; set; }

  

    }
}
