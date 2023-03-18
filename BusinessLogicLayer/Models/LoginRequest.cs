using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class LoginRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
