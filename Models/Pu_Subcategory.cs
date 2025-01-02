
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP
{
    [Table("Pu_Subcategory")]
    public class Subcategory
    {

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        
        [ForeignKey("Maincategory")]

        [Column("Maincategory_id")]
        public Guid Maincategory_id { get; set; }
        public Maincategory Maincategories { get; set; }


        [Required]
        [Column("name")]
        public string name { get; set; }

        
        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Organization_id")]
        public Guid Organization_id { get; set; }

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public Guid user_id { get; set; }
       

        [Required]
        [Column("state")]
        public bool state{ get; set; } = false;


        public ICollection<Item> Items {get; set; }
        
 

    }
}
