using DiIocRehearsal.Contracts;

namespace DiIocRehearsal.ServicesWithContract
{
    public class DiskFileService : IFileRepository
    {
        const string basePath = "C:\\Temp\\uploaded-images\\";

        public async Task<string> WriteAsync(string name, string base64)
        {
            var fileName = $"{DateTime.Now.ToString("ffffff")}_{name}";
            var filePath = $"{basePath}{fileName}";
            var fileBytes = Convert.FromBase64String(base64);

            using var fileDiskStream = new FileStream(filePath, FileMode.OpenOrCreate);
            await fileDiskStream.WriteAsync(fileBytes, 0, fileBytes.Length);

            return fileName;
        }

        public async Task<byte[]> ReadAsync(string fileName)
        {
            var filePath = $"{basePath}{fileName}";
            using var fileStream = new FileStream(filePath, FileMode.Open);
            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }
    }
}