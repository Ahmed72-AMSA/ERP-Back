using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace erp_back.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(CompanyName), IsUnique = true)]
    public class Authentication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(100)]
        public string? CompanyName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        public string? FilePath { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }

        private string? _password;

        [Required(ErrorMessage = "Password is required")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string? Role { get; set; } = "admin";
    }
}
