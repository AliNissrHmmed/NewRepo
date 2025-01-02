

using Microsoft.AspNetCore.Mvc;

namespace ERP
{
   
    public class CompanyDto
    {



        public string name { get; set; }
   
        public string phone { get; set; }

        public string phone2 { get; set; }

        public string address { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid Organization_id { get; set; }

        public Guid user_id { get; set; }

        public bool State { get; set; }

        public Guid Maincategory_id { get; set; }

        //public ICollection<AttachmentDto> Attachments { get; set; }
        public IEnumerable<IFormFile>Attachments { get; set; }


    }
}
