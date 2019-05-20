using System.Threading.Tasks;

namespace WebApi.BusinessLogic.Helpers.Interfaces
{
    public interface IUserFilesHelper
    {
        Task<int> WriteFile(string username, string fileName, byte[] chunk, long contentLength);
    }
}
