using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationDonation.Models
{
    public class DonationViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome da doação")]
        public string DonationName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Remote(action: "IsZipCodeValid", controller: "Donation", AdditionalFields = "Id")]
        [Display(Name = "CEP para entrega ou retirada")]
        [StringLength(maximumLength: 15, MinimumLength = 3)]
        public string DonationZipCode { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Registro")]
        public DateTime DateOfRegister { get; set; }

        [Display(Name = "Valor do frete")] 
        public double CourierPrice { get; set; }

        [Required]
        [Range(0, 100)]
        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "O bem doado é novo?")]
        public bool NewOrOld { get; set; }

        [Required] 
        [Display(Name = "Usuário")] 
        public int UserId { get; set; }
        public UserViewModel User { get; set; }

    }
}
