namespace ERP.PURCHASES.Dto
{
   


    public class PaginatedResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public PaginationData Metadata { get; set; }
    }
    public class PaginationData
    {

        public int totalCount { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }

    }
}
