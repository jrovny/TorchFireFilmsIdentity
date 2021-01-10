using Microsoft.AspNetCore.Identity;
using System;

namespace TorchFireFilms.Identity.Data
{
    public class ApplicationUserClaim : IdentityUserClaim<int>
    {
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedUserId { get; set; }
        public bool Deleted { get; set; }
    }
}
