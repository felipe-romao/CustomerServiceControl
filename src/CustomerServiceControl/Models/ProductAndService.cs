using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerServiceControl.Models
{
    public class ProductAndService
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }
        [MaxLength(50)]
        [Display(Name = "Tipo")]
        public string Type { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}