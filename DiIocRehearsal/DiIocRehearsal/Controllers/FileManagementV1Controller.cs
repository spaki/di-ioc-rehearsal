using DiIocRehearsal.Dtos.Request;
using DiIocRehearsal.Dtos.Result;
using Microsoft.AspNetCore.Mvc;

namespace DiIocRehearsal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileManagementV1Controller : ControllerBase
    {
        [HttpPost]
        public FileUploadedResult Post(FileRequest request)
        {
            var fileName = $"{DateTime.Now.ToString("ffffff")}_{request.Name}";
            var filePath = $"C:\\Temp\\uploaded-images\\{fileName}";
            var fileBytes = Convert.FromBase64String(request.Base64);
            
            using var fileDiskStream = new FileStream(filePath, FileMode.OpenOrCreate);
            fileDiskStream.Write(fileBytes, 0, fileBytes.Length);

            return new FileUploadedResult { FileName = fileName };
        }

        [HttpGet]
        public IActionResult Get(string fileName)
        {
            var filePath = $"C:\\Temp\\uploaded-images\\{fileName}";
            using var stream = new FileStream(filePath, FileMode.Open);

            if (stream == null)
                return NotFound(); 

            return File(stream, "application/octet-stream", fileName);
        }
    }
}
