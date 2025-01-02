using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERP.PURCHASES.Models
{
    public class Template
{

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid  id { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("nickname")]    
        public string nickname { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; }

        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }

        [Column("state")]
        public bool state { get; set; }

        public ICollection<Su_members> su_Members { get; set; }
    }
}
