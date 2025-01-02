using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP
{
    [Table("Or_Sections")]
    public class Section
    {

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }



        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public Guid user_id { get; set; }


        [Required]
        [Column("name")]
        public string name { get; set; }
       

        [Required]
        [Column("state")]
        public bool State { get; set; } = true;


        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;



       
        public string serial { get; set; }

       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Organization_id")]
        public Guid Organization_id { get; set; }

       public ICollection<Ma_Subgroup> Ma_Subgroups { get; set; }
        public ICollection<Ma_subjoin> Ma_subjoins { get; set; }


    }
}
