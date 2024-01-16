namespace DiIocRehearsal.Services
{
    public class DiskFileService
    {
        const string basePath = "C:\\Temp\\uploaded-images\\";

        public string Write(string name, string base64) 
        {
            var fileName = $"{DateTime.Now.ToString("ffffff")}_{name}";
            var filePath = $"{basePath}{fileName}";
            var fileBytes = Convert.FromBase64String(base64);

            using var fileDiskStream = new FileStream(filePath, FileMode.OpenOrCreate);
            fileDiskStream.Write(fileBytes, 0, fileBytes.Length);

            return fileName;
        }

        public byte[] Read(string fileName)
        {
            var filePath = $"{basePath}{fileName}";
            using var fileStream = new FileStream(filePath, FileMode.Open);
            using var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
