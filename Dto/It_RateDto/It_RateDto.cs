namespace ERP


{

public class CreateIt_RateDto
{

        public int range { get; set; }

        public Guid user_id { get; set; }

        public Guid Item_id { get; set; }

        public string note { get; set; }
        public bool State { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Type { get; set; }

}
    public class UpdateIt_RateDto
    {

        public int? range { get; set; }

       

        public string? note { get; set; }
        public bool? State { get; set; } 

        public string? Type { get; set; }

    }

}