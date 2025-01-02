
namespace ERP
{


 public class CreateItemsDto
    {

        public string? name { get; set; }     

        public string? Type { get; set; }

    }



    public class UpdateItemsDto
    {

        public string? name { get; set; }

        public string? Type { get; set; }
        public bool? state { get; set; }

        public Guid? company_id { get; set; }

        public Guid? Subcategory_id { get; set; }

    }
    public class ItemsDto
    {


        public string? name { get; set; }

        public Guid company_id { get; set; }

      

        public Guid Subcategory_id { get; set; }



        public DateTime CreatedAt { get; set; } = DateTime.Now;

    

        
        public Guid user_id { get; set; }
       

        public bool State { get; set; } = true;


        public string? Type { get; set; }

    }
}