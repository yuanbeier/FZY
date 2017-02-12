using System.ComponentModel.DataAnnotations;

namespace FZY.Web.Models.Account
{
    public class LoginViewModel
    {

        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

    }
}