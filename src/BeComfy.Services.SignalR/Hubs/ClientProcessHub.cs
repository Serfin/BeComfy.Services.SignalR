
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BeComfy.Services.SignalR.Hubs
{
    public class ClientProcessHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}