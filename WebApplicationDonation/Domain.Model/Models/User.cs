using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        public string UserName { get; set; }
        [Display(Name = "Sobrenome")]
        public string UserLastName { get; set; }
        [Display(Name = "Data de registro")]
        public DateTime DateOfRegister { get; set; }
        [Display(Name = "Data de aniversário")]
        public DateTime UserBirthday { get; set; }
        [Display(Name = "É pessoa física?")]
        public bool UserLegalStatus { get; set; }
        [Display(Name = "Endereço")]
        public string UserAddress { get; set; }
        [Display(Name = "Complemento")]
        public string UserAddressDetails { get; set; }
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        [Display(Name = "Tempo em que doa (em anos)")]
        public double UserTimeInDonation { get; set; }
        [Display(Name = "Número de doações")]
        public int QuantityDonations { get; set; }
        public List<Donation> Donations { get; set; }

    }
}
