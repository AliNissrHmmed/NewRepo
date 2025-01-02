using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP 
{

    [Table ("Or_Suborder")]
 public class Or_Suborder
{


        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid UniqueID { get; set; } = Guid.NewGuid();



        [ForeignKey("Item")]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Item_id")]
        public Guid Item_id { get; set; }

        public Item Items { get; set; }

        public string Code { get; set; } // This links to the Code column in OrOrder

         public int Quantity { get; set; }
        public decimal Price { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool State { get; set; }=false;

        // Navigation Properties
       public Or_Order Orders { get; set; }
     
}

}
