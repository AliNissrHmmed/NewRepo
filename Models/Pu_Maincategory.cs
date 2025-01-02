
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP
{
    [Table("Pu_Maincategory")]
    public class Maincategory
    {

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        
        [Required]
        [Column("name")]
        public string name { get; set; }

        
        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Organization_id")]
        public Guid? Organization_id { get; set; }

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public Guid? user_id { get; set; }
       

        
        [Column("state")]
        public bool state{ get; set; } = false;

        public ICollection <Subcategory> Subcategories {get; set; }

        public ICollection<Company> Companies { get; set; }


    }
}
