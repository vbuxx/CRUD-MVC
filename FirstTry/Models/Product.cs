using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTry.Models
{
    public class Product
    {
        
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Stock is required.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Proce is required.")]
        public int Price { get; set; }
    }
}
