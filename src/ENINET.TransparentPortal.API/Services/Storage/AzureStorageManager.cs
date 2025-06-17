

namespace ENINET.TransparentPortal.API.Services.Storage
{
    public class AzureStorageManager : IStorageManager
    {
        public Task<bool> DeleteFile(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> Download(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveFile(string root, string fileName, byte[] fileContent)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveFile(string root, string fileName, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
