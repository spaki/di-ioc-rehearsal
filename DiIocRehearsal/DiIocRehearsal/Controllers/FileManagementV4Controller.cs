using DiIocRehearsal.Contracts;
using DiIocRehearsal.Dtos.Request;
using DiIocRehearsal.Dtos.Result;
using DiIocRehearsal.ServicesWithContract;
using Microsoft.AspNetCore.Mvc;

namespace DiIocRehearsal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileManagementV4Controller : ControllerBase
    {
        private readonly IFileRepository service;

        public FileManagementV4Controller()
        {
            //this.service = new DiskFileService();
            this.service = new AzureFileService();
        }

        [HttpPost]
        public async Task<FileUploadedResult> Post(FileRequest request)
        {
            var fileNameResult = await service.WriteAsync(request.Name, request.Base64);

            return new FileUploadedResult{ FileName = fileNameResult };
        }

        [HttpGet]
        public async Task<IActionResult> Get(string fileName)
        {
            var bytes = await service.ReadAsync(fileName);
            
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
