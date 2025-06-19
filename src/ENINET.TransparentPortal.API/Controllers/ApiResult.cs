namespace ENINET.TransparentPortal.API.Controllers
{
    public record class PagedInfo(int totalItems, int numPages, int pageSize, int currentPage);

    public class ApiResult<T>
    {
        public T Data { get; set; } = default!;
        public int StatusCode { get; set; } = default!;
        public string Message { get; set; } = default!;

        public PagedInfo? PageInfo { get; set; } = default!;
    }
}
