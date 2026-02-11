using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientManager.Data.Entities
{
    [Table("Notes")]
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(50)]
        public string Category { get; set; } // General, Compliance, Meeting, PhoneCall

        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
    }
}
