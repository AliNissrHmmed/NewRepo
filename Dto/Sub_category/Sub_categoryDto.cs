namespace ERP

{
    public class Sub_categoryDto
{
        public string name { get; set; }

       

        public Guid maincategory_id { get; set; }

       
}

    public class UpdateSub_categoryDto
    {
        public string? name { get; set; }

        public bool? state { get; set; }
 


    }
}
