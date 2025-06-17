namespace ENINET.TransparentPortal.API.Services.Storage
{
    public interface IStorageManager
    {
        public Task<bool> SaveFile(string root, string fileName, byte[] fileContent);
        public Task<bool> SaveFile(string root, string fileName, Stream stream);
        public Task<bool> DeleteFile(string path, string fileName);
        public Task<bool> Exists(string path, string fileName);

        public Task<byte[]> Download(string path, string fileName);

    }
}
