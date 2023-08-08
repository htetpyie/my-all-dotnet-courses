using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HPPMDotNetCore.MvcApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task ServerSendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ClientReceiveMessage", user, message);
        }
    }
}
