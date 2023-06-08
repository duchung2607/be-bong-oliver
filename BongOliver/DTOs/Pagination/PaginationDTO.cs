namespace BongOliver.DTOs.Pagination
{
    public class PaginationDTO<T>
    {
        public List<T> data {  get; set; }
        public int totalCount { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 5;
    }
}
