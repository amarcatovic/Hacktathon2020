using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Unjumble.Core.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
