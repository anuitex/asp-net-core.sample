using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApi.BusinessLogic.Helpers.Interfaces;

namespace WebApi.BusinessLogic.Hubs
{
    public class FileTransferHub: Hub
    {
        private readonly IUserFilesHelper _userFilesHelper;

        public FileTransferHub(IUserFilesHelper userFilesHelper)
        {
            _userFilesHelper = userFilesHelper;
        }

        public async Task ReceiveFileChunk(string username, string fileName, byte[] chunk, long contentLength)
        {
            int progress = await _userFilesHelper.WriteFile(username, fileName, chunk, contentLength);
            await Clients.Caller.SendAsync("file-transfer-progress", fileName, progress);
        }
    }
}
