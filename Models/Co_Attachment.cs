using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace ERP
{
    [Table("Co_Attachment")]
    public class Attachment
    {

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }



        [ForeignKey("Companies")]

        [Column("company_id")]
        public Guid company_id { get; set; }
        public Company Companies { get; set;}

        
        

        [Column("url")]
        public string url { get; set; }


        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public Guid ?user_id { get; set; }
       

        [Required]
        [Column("state")]
        public bool state { get; set; } = true;

    }
}
