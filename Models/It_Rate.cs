using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP

{
    [Table("It_Rate")]
    public class Rate
    {

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        public int range { get; set; }



        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public Guid user_id { get; set; }


        [ForeignKey ("Item")]
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Item_id")]
        public Guid Item_id { get; set; }

        public Item Items { get; set; }


        public string note { get; set; }
       

        [Required]
        [Column("state")]
        public bool State { get; set; } = true;


        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("Type")]
        public string? Type { get; set; }

       
 

    }
}
