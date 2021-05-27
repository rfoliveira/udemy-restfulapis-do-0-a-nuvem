using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class FileBusiness : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusiness(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string filename) 
        {
            var filePath = _basePath + filename;

            if (!File.Exists(filePath))
                return null;
            
            return File.ReadAllBytes(filePath);
        } 

        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            var list = new List<FileDetailVO>();
            foreach (var file in files)
            {
                list.Add(await SaveFileToDisk(file));
            }

            return list;
        }

        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            if (file == null || file?.Length == 0)
                return null;

            var fileDetail = new FileDetailVO();
            var fileType = Path.GetExtension(file.FileName);
            //  Pega a url baseado no host
            var baseUrl = _context.HttpContext.Request.Host.Host;

            if (fileType.ToLower() == ".pdf" 
                || fileType.ToLower() == ".jpg"
                || fileType.ToLower() == ".jpeg"
                || fileType.ToLower() == ".png")
            {
                var docName = Path.GetFileName(file.FileName);
                var destination = Path.Combine(_basePath, docName);

                fileDetail.DocumentName = docName;
                fileDetail.Doctype = fileType;
                fileDetail.DocUrl = Path.Combine(baseUrl, "/api/v1/file/", fileDetail.DocumentName);

                using (var stream = new FileStream(destination, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return fileDetail;
        }
    }
}