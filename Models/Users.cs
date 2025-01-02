using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERP.PURCHASES.Models
{
    public class Users
{

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid id { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("password")]
        public string password { get; set; }


        [Column("serial")]
        public string serial { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; }


        [Column("state")]
        public bool state { get; set; }

        public ICollection<Ma_subjoin> Ma_subjoins { get; set; }

        public ICollection<Or_Maingroup>Or_Maingroups { get; set; }

        public ICollection<Ma_Subgroup> Ma_Subgroups { get; set; }
    }
}
