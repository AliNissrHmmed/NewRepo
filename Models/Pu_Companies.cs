
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP
{
    [Table("Pu_Companies")]
    public class Company
    {

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("name")]
        public string name { get; set; }


        [ForeignKey("Maincategory")]

        [Column("Maincategory_id")]
        public Guid Maincategory_id { get; set; }
        public Maincategory Maincategories { get; set; }

        [Column("phone")]
        public string phone { get; set; }


        [Column("phone2")]
        public string phone2 { get; set; }


        [Required]
        [Column("address")]
        public string address { get; set; }

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
        public bool State { get; set; } = true;

        public ICollection<Attachment> Attachments { get; set; }

        public ICollection<Item>Items { get; set; }

        public ICollection<Subcategory> Subcategories { get; set; }

 

        
    }
}
