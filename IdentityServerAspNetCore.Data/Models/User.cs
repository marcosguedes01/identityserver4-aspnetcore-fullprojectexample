using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerAspNetCore.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Claim> Claims { get; set; }

        public User()
        {
            Claims = new List<Claim>
                    {
                        new Claim("email", "usertestinmodel@domain.com")
                    };
        }
    }
}
