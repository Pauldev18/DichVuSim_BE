using Microsoft.AspNetCore.SignalR;
using DichVuSim_BE.DTO;
namespace DichVuSim_BE
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(PhanHoiDTO message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
