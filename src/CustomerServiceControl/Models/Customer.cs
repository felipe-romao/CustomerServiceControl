using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CustomerServiceControl.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [MaxLength(100)]
        [Display(Name = "Empresa")]
        public string CompanyName { get; set; }

        [MaxLength(12)]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [MaxLength(100)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Column(TypeName = "text")]
        [Display(Name = "Conversa")]
        [DataType(DataType.MultilineText)]
        public string Conversation { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data Primeiro Atendimento")]
        public DateTime DateTimeFirstAttendance { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data Último Atendimento")]
        public DateTime DateTimeLastAttendance { get; set; }

        [Display(Name = "Produtos/Serviços")]
        public virtual ICollection<ProductAndService> ProductsAndServices { get; set; }

        public void SetDateTimeNowFirstAttendance()
        {
            this.DateTimeFirstAttendance = DateTime.Now;
        }

        public void SetDateTimeNowLastAttendance()
        {
            this.DateTimeLastAttendance = DateTime.Now;
        }
    }
}