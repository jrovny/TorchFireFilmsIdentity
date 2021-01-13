using System;
using Microsoft.AspNetCore.Identity;

namespace TorchFireFilms.Identity.Data
{
    public class ApplicationUser : IdentityUser<int>
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
