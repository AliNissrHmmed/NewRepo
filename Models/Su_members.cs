using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERP.PURCHASES.Models
{
    public class Su_members
{



        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }


        [ForeignKey("Template")]
        public Guid temp_id { get; set; }
        public Template Templates { get; set; }


        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;




        [ForeignKey("User")]
        public Guid user_id { get; set; }
        public Users User { get; set; }



        [Column("code")]
        public Guid  code { get; set; }
        


        [Required]
        [Column("state")]
        public bool state { get; set; } = false;
    }
}
