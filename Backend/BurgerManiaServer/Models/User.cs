using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerManiaServer.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId {  get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserPassword { get; set; }
        public override string ToString()
        {
            return $"User Id: ${UserId}, User name: ${UserName}, Password: ${UserPassword}";
        }
    }
}
