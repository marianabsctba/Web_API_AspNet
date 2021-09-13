using System.Collections.Generic;

namespace WebApplicationDonation.Models
{
    public class UserIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
