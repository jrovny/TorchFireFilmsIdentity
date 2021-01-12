using System;
using Microsoft.AspNetCore.Identity;

namespace TorchFireFilms.Identity.Data
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
