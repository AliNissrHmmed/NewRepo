namespace ERP
{
   public class MaSubgroupDto
{

    public Guid SectionId { get; set; }

    public string Code { get; set; }
    public string Note { get; set; }
    public string ItemType { get; set; }
    public bool SupTreeGroup { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public bool State { get; set; }
}

    public class Update_MaSubgroupDto
    {

      
        public string? Code { get; set; }
        public string? Note { get; set; }
        public string? ItemType { get; set; }
        public bool? SupTreeGroup { get; set; }
        public bool? State { get; set; }
    }
}
