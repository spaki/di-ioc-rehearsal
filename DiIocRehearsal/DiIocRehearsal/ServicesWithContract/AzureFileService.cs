using Azure.Storage.Blobs;
using DiIocRehearsal.Contracts;

namespace DiIocRehearsal.ServicesWithContract
{
    public class AzureFileService : IFileRepository
    {
        const string storageConnString = "";
        const string containerName = "diiocrehearsal";

        private readonly BlobServiceClient blobServiceClient;
        private readonly BlobContainerClient containerClient;
        public AzureFileService()
        {
            this.blobServiceClient = new BlobServiceClient(storageConnString);
            this.containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task<string> WriteAsync(string name, string base64)
        {
            var fileName = $"{DateTime.Now.ToString("ffffff")}_{name}";
            var fileBytes = Convert.FromBase64String(base64);

            var blobClient = containerClient.GetBlobClient(fileName);

            using var memoryStream = new MemoryStream(fileBytes);
            await blobClient.UploadAsync(memoryStream);

            return fileName;
        }

        public async Task<byte[]> ReadAsync(string fileName)
        {
            var blobClient = containerClient.GetBlobClient(fileName);
            var response = await blobClient.DownloadAsync();

            using var memoryStream = new MemoryStream();
            response.Value.Content.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
