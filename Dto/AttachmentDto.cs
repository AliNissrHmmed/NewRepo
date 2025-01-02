using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERP
{
    public class AttachmentDto

{


        public Guid company_id { get; set; }
        




        
       // public string url { get; set; }


       
        //public DateTime CreatedAt { get; set; } = DateTime.Now;



        
        public Guid ?user_id { get; set; }

 
        public bool state { get; set; }

        public List<IFormFile> url { get; set; }
    }


    public class UpdateAttachmentDto

    {


        public Guid company_id { get; set; }






        // public string url { get; set; }



        //public DateTime CreatedAt { get; set; } = DateTime.Now;




        public Guid? user_id { get; set; }


        public bool state { get; set; }

        public List<IFormFile> url { get; set; }
    }
}
