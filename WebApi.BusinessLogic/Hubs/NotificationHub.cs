using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.BusinessLogic.Hubs
{
    public class NotificationHub: Hub
    {
        public async Task ServerSubscription(string message)
        {
            await Clients.All.SendAsync("clientSubscription", message);
        }
    }
}
