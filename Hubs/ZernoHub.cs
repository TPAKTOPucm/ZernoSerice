using Microsoft.AspNetCore.SignalR;

namespace Zerno.Hubs
{
    public class ZernoHub : Hub
    {
        public async Task NotifyWebUsers(string user, string message)
        {
            await Clients.All.SendAsync("DisplayNotification", user, message);
        }
    }
}
