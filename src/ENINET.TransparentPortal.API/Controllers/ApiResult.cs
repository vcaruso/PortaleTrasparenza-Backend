namespace ENINET.TransparentPortal.API.Controllers
{
    public class ApiResult<T>
    {
        public T Data { get; set; } = default!;
        public int StatusCode { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}
