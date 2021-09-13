using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationDonation.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Sobrenome")]
        public string UserLastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Registro")]
        public DateTime DateOfRegister { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de aniversário")]
        public DateTime UserBirthday { get; set; }

        [Required]
        [Display(Name = "É pessoa física?")]
        public bool UserLegalStatus { get; set; }

        [Required]
        [Display(Name = "Endereço")]
        [StringLength(100)]
        public string UserAddress { get; set; }

        [Required]
        [Display(Name = "Complemento")]
        [StringLength(100)]
        public string UserAddressDetails { get; set; }

        [Required]
        [Remote(action: "IsCpfValid", controller: "User", AdditionalFields = "Id")]
        [Display(Name = "CPF")]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string Cpf { get; set; }

        public string FullName => $"{Id} - {UserName} {UserLastName}";

        [Display(Name = "Há quanto tempo (em anos) você faz doações?")]
        public double UserTimeInDonation { get; set; }

        [Display(Name = "Quantidade de doações")]
        public int QuantityDonations { get; set; }
        public List<DonationViewModel> Donations { get; set; }


    }
}
