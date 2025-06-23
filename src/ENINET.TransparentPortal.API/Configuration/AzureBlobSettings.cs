namespace ENINET.TransparentPortal.API.Configuration
{
    public class AzureBlobSettings
    {
        public string Tenant_Id { get; set; } = default!;
        public string Client_Id { get; set; } = default!;

        public string Client_Secret { get; set; } = default!;

        public string StorageAccountName { get; set; } = default!;

        public string ContainerName { get; set; } = default!;

    }
}
