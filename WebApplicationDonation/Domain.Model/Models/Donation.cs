using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Models
{
    public class Donation
    {

        public int Id { get; set; }

        [Display(Name = "Nome da doação")]
        public string DonationName { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Data de registro")]
        public DateTime DateOfRegister { get; set; }

        [Display(Name = "Valor do frete")]
        public double CourierPrice { get; set; }
        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }
        [Display(Name = "A doação é nova?")]
        public bool NewOrOld { get; set; }
        [Display(Name = "CEP")]
        public string DonationZipCode { get; set; }
        [Display(Name = "Id de usuário")]
        public int UserId { get; set; }
        public User User { get; set; }


    }
}
