using AutoMapper.Configuration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos.Account
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        //[RegularExpression("(?=^.{6,10}$)(?=.*\\d)( ?=.* [a-z])( ?=.* [A-Z]) ( ?=.* [!@#$%^&amp ;* ()_+}{&quot ;:; '?/&gt; .&lt;, ])( ?!.* \\\\s) .* $\",")]
        public string Password { get; set; }
    }
}
