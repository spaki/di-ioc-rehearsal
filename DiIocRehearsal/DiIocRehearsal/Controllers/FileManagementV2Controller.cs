using DiIocRehearsal.Dtos.Request;
using DiIocRehearsal.Dtos.Result;
using DiIocRehearsal.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiIocRehearsal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileManagementV2Controller : ControllerBase
    {
        [HttpPost]
        public FileUploadedResult Post(FileRequest request)
        {
            var service = new DiskFileService();
            var fileNameResult = service.Write(request.Name, request.Base64);

            return new FileUploadedResult{ FileName = fileNameResult };
        }

        [HttpGet]
        public IActionResult Get(string fileName)
        {
            var service = new DiskFileService();
            var bytes = service.Read(fileName);
            
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
