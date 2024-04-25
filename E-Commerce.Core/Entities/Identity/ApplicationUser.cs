using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;

namespace E_Commerce.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
