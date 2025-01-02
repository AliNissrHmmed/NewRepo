

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERP
{
    public class SectionDto
    {
        public Guid Id { get; set; }


        public Guid user_id { get; set; }


        public string name { get; set; }


        public bool State { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;



        public string serial { get; set; }


        public Guid Organization_id { get; set; }

    }


    public class  CreateSectionDto
    {


        public string name { get; set; }
    }

    public  class UpdateSectionDto
    {

        
        public string? name { get; set; }

        public bool? state { get; set; }

 

    }





}
