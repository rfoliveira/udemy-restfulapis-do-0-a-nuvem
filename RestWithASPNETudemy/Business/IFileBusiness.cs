using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Business
{
    public interface IFileBusiness
    {
         byte[] GetFile(string filename);
         Task<FileDetailVO> SaveFileToDisk(IFormFile file);
         Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}