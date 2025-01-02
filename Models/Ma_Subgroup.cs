using ERP.PURCHASES.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP
{

    [Table("Ma_Subgroup")]
    public class Ma_Subgroup
{

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

       


        [ForeignKey("sections")]
       
        public  Guid SectionId { get; set; }
        public Section sections { get; set; }



    /*    [ForeignKey("maingroups")]

        [Column("maingroup_id")]
        public Guid maingroup_id { get; set; }
        public Or_Maingroup maingroups { get; set; }
*/

        [Required]
        [Column("code")]
        public Guid code { get; set; }


        [Required]
        [Column("note")]
        public string note { get; set; }  




     
        [Column("itemtype")]
        public string itemtype { get; set; }



        [Required]
        [Column("suptreegroup")]
        public bool suptreegroup { get; set; }



        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;




        


        [ForeignKey("User")]
        public Guid user_id { get; set; }
        public Users User { get; set; }


        [Required]
        [Column("state")]
        public bool State { get; set; } = false;

        public ICollection<Ma_subjoin> Ma_subjoins { get; set; }
       //public ICollection<Or_Maingroup> or_Maingroups { get; set; }
    }
}
