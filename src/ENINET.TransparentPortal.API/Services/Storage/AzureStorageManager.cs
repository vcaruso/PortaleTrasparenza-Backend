

using Azure.Identity;
using Azure.Storage.Blobs;
using ENINET.TransparentPortal.API.Configuration;

namespace ENINET.TransparentPortal.API.Services.Storage
{
    public class AzureStorageManager : IStorageManager
    {
        private readonly IConfiguration _configuration;

        private readonly AzureBlobSettings _azureStorageSettings = new AzureBlobSettings();

        public AzureStorageManager(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _configuration.Bind("AzureBlob", _azureStorageSettings);

        }

        public async Task<bool> DeleteFile(string path, string fileName)
        {
            var segmenti = Utility.ReportUtility.SplitFileNameToPath(fileName);
            var percorso = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine("sites", segmenti.Site), segmenti.Area), segmenti.Year), segmenti.Month), fileName);
            var blobClient = await GetBlobClientAsync(percorso);
            if (await blobClient.ExistsAsync())
            {
                await blobClient.DeleteAsync();
                return true;
            }
            throw new FileNotFoundException();
        }

        public async Task<byte[]> Download(string path, string fileName)
        {
            var segmenti = Utility.ReportUtility.SplitFileNameToPath(fileName);
            var percorso = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine("sites", segmenti.Site), segmenti.Area), segmenti.Year), segmenti.Month), fileName);
            var blobClient = await GetBlobClientAsync(percorso);
            using (var stream = new MemoryStream())
            {
                await blobClient.DownloadToAsync(stream);
                byte[] buffer;
                buffer = stream.ToArray();
                return buffer;

            }


        }

        public Task<bool> Exists(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveFile(string root, string fileName, byte[] fileContent)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveFile(string root, string fileName, Stream stream)
        {
            var percorsi = Utility.ReportUtility.SplitFileNameToPath(fileName);
            var percorso = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine("sites", percorsi.Site), percorsi.Area), percorsi.Year), percorsi.Month), fileName);
            var blobClient = await GetBlobClientAsync(percorso);
            await blobClient.UploadAsync(stream, overwrite: true);
            return true;
        }

        public async Task<BlobClient> GetBlobClientAsync(string fileName)
        {
            string storageAccountUrl = $"https://{_azureStorageSettings.StorageAccountName}.blob.core.windows.net/";

            // Create a ClientSecretCredential for authentication
            var credential = new ClientSecretCredential(_azureStorageSettings.Tenant_Id, _azureStorageSettings.Client_Id, _azureStorageSettings.Client_Secret);
            // Create a BlobServiceClient using the credential
            var blobServiceClient = new BlobServiceClient(new Uri(storageAccountUrl), credential);
            // Get a reference to the container
            var containerClient = blobServiceClient.GetBlobContainerClient(_azureStorageSettings.ContainerName);
            // Get a reference to the blob
            return await Task.FromResult(containerClient.GetBlobClient(fileName));


        }


    }
}
