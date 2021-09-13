using System.Collections.Generic;

namespace WebApplicationDonation.Models
{
    public class DonationIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<DonationViewModel> Donations { get; set; }
    }
}
