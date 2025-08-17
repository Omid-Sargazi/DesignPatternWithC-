using Microsoft.AspNetCore.Identity;

namespace SMSAuthentication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Message { get; set; }
       
    }
}
