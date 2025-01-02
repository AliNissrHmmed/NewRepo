using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ERP 
{

    [Table ("Or_order")]
  public class Or_Order
{

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }


         public string Code { get; set; } // This will be used to relate to OrSuborder

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid OrganizationId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool State { get; set; } = false;

  
    
       public ICollection<Or_Suborder> Suborders { get; set; }

/*
        public async Task<IEnumerable<OrOrder>> GetOrdersWithSubordersAsync()
        {
            return await _context.Orders
                .Include(o => o.Suborders)
                .Where(o => o.Suborders.Any(s => s.Code == o.Code))
                .ToListAsync();
        }
*/
    }

}
