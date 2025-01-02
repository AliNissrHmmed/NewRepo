using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ERP.PURCHASES.Models;

namespace ERP
{
    [Table("Or_Maingroup")]
    public class Or_Maingroup



    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("code")]
        public Guid code { get; set; }


        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Organization_id")]
        public Guid Organization_id { get; set; }


        [ForeignKey("User")]
        public Guid user_id { get; set; }
        public Users User { get; set; }

        [Required]
        [Column("state")]
        public bool State { get; set; } = false;

        public ICollection<Ma_subjoin> Ma_subjoins { get; set;}
        public ICollection<Ma_Subgroup> Ma_Subgroups { get; set; }

    }
}
