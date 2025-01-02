
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP
{
    [Table("Co_Items")]
    public class Item
    {

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("name")]
        public string name { get; set; }

        [ForeignKey("Companies")]

        [Column("company_id")]
        public Guid company_id { get; set; }
        public Company Companies { get; set; }


        [ForeignKey("Subcategory")]

        [Column("Subcategory_id")]
        public Guid Subcategory_id { get; set; }
        public Subcategory Subcategories { get; set; }


        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public Guid user_id { get; set; }
       

        [Required]
        [Column("state")]
        public bool State { get; set; } = true;


        public string Type { get; set; }
        
        public ICollection<Rate>Rates{get; set; }

        public ICollection<Or_Suborder> OrSuborderes { get; set; }



    }
}
