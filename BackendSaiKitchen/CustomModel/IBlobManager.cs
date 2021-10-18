using BackendSaiKitchen.Models;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public interface IBlobManager
    {
        Task Upload(Blob File);

        Task<byte[]> Read(string fileName);

        Task PostAsync(Blob File);

        Task Delete(string FileName);
    }
}