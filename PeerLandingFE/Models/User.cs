// Models/User.cs
using System.ComponentModel.DataAnnotations;

namespace PeerLandingFE.Models
{
    public class User
    {
        public int Id { get; set; } // Misalkan ini adalah primary key
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; } // Pastikan untuk mengenkripsi password sebelum menyimpannya
        [Required]
        public string Role { get; set; }
        public decimal Balance { get; set; } = 0; // Nilai default untuk balance
    }
}
