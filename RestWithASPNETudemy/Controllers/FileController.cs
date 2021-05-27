using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FileController : ControllerBase
    {
      private readonly ILogger<FileController> _logger;
      private IFileBusiness _business;

        public FileController(ILogger<FileController> logger, IFileBusiness business)
        {
            _logger = logger;
            _business = business;
        }

        [HttpPost]
        [Route("uploadfile")]
        [ProducesResponseType(typeof(FileDetailVO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces("application/json")]  // opcional
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            var detail = await _business.SaveFileToDisk(file);

            if (detail == null)
                return NotFound("File not found or empty");

            return Ok(detail);
        }

        [HttpPost]
        [Route("uploadfiles")]
        [ProducesResponseType(typeof(List<FileDetailVO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces("application/json")]  // opcional
        public async Task<IActionResult> UploadMultipleFile([FromForm] List<IFormFile> files)
        {
            var details = await _business.SaveFilesToDisk(files);

            if (details == null)
                return NotFound("Files not found or empty");

            return Ok(details);
        }

        [HttpGet]
        [Route("downloadfile/{filename}")]
        [ProducesResponseType(typeof(byte[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return BadRequest("invalid client request");

            var file = _business.GetFile(filename);

            if (file == null)
                return NotFound("File not found");

            HttpContext.Response.ContentType = $"application/{Path.GetExtension(filename).Replace(".", "")}";
            HttpContext.Response.Headers.Add("content-length", file.Length.ToString());
            await HttpContext.Response.Body.WriteAsync(file, 0, file.Length);

            return new ContentResult();
        }
    }
}