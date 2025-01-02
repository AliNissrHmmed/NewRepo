using static System.Collections.Specialized.BitVector32;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ERP.PURCHASES.Models;

namespace ERP
{
    [Table("Ma_subjoin")]
    public class Ma_subjoin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [ForeignKey("Or_Maingroups")]
        public Guid MaingroupId { get; set; }
        public Or_Maingroup Or_Maingroups { get; set; }

        [ForeignKey("Ma_Subgroups")]
        public Guid SubgroupId { get; set; }
        public Ma_Subgroup Ma_Subgroups { get; set; }

        [ForeignKey("Sections")]
        public Guid section_id { get; set; }
        public Section Sections { get; set; }

        [ForeignKey("User")]
        public Guid user_id { get; set; }
        public Users User { get; set; }
        public  Guid  code  { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;  

        
        public bool State { get; set; }= false; 

       
        public string Name { get; set; }
    }
}
