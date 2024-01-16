using DiIocRehearsal.Contracts;
using DiIocRehearsal.Dtos.Request;
using DiIocRehearsal.Dtos.Result;
using Microsoft.AspNetCore.Mvc;

namespace DiIocRehearsal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileManagementV6Controller : ControllerBase
    {
        private readonly IEnumerable<IFileRepository> services;

        public FileManagementV6Controller(IEnumerable<IFileRepository> services)
        {
            this.services = services;
        }

        [HttpPost]
        public async Task<FileUploadedResult> Post(FileRequestV2 request)
        {
            var fileNameResult = await GetServiceByKey(request.ProviderKey).WriteAsync(request.Name, request.Base64);

            return new FileUploadedResult{ FileName = fileNameResult };
        }

        [HttpGet]
        public async Task<IActionResult> Get(string fileName, string providerKey)
        {
            var bytes = await GetServiceByKey(providerKey).ReadAsync(fileName);
            
            return File(bytes, "application/octet-stream", fileName);
        }

        private IFileRepository GetServiceByKey(string key) => services.FirstOrDefault(e => e.GetType().Name.StartsWith(key));
    }
}
