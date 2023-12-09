using Microsoft.AspNetCore.SignalR;

namespace ProniaBackEnd.Hubs
{
    public class LiveStreamHub : Hub
    {
        public async Task SendVideoStream(byte[] videoData)
        {
            await Clients.All.SendAsync("ReceiveVideoStream", videoData);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
