using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientManager.Data.Entities
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } // Active, Inactive, Prospect

        public DateTime DateOnboarded { get; set; }

        [StringLength(50)]
        public string RiskProfile { get; set; } // Low, Medium, High

        public string Notes { get; set; }

        // Navigation properties
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Note> ClientNotes { get; set; }

        // Computed — legacy style, no DTO
        [NotMapped]
        public string FullName => FirstName + " " + LastName;
    }
}
