using System;
using Microsoft.AspNetCore.Identity;

namespace TorchFireFilms.Identity.Data
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
