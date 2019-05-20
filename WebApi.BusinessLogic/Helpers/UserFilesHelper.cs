using System.IO;
using System.Threading.Tasks;
using WebApi.BusinessLogic.Helpers.Interfaces;

namespace WebApi.BusinessLogic.Helpers
{
    public class UserFilesHelper: IUserFilesHelper
    {
        private string GetDirectory(string username)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            var currentDirectoryInfo = new DirectoryInfo(currentDirectory);
            string currentDirectoryParentDirectory = currentDirectoryInfo.Parent.FullName;

            string userFilesDirectory = Path.Combine(currentDirectoryParentDirectory, "UsersFiles", username);

            if (!Directory.Exists(userFilesDirectory))
            {
                Directory.CreateDirectory(userFilesDirectory);
            }

            return userFilesDirectory;
        }

        public async Task<int> WriteFile(string username, string fileName, byte[] data, long contentLength)
        {
            string userDirectoryPath = GetDirectory(username);
            string pathToFile = Path.Combine(userDirectoryPath, fileName);

            using (var fileStream = new FileStream($"{pathToFile}", FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                long streamLength = fileStream.Length;

                if (streamLength >= contentLength)
                {
                    return 100;
                }

                await fileStream.WriteAsync(data, 0, data.Length);

                int progress = (int)((double)streamLength / contentLength * 100);
                return progress;
            }
        }
    }
}
