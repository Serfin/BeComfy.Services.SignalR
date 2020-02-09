
using System;
using System.Threading.Tasks;
using BeComfy.Common.Authentication;
using BeComfy.Services.SignalR.Operations;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BeComfy.Services.SignalR.Hubs
{
    public class ClientProcessHub : Hub
    {
        private readonly IJwtHandler _jwtHandler;

        public ClientProcessHub(IJwtHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        public async Task InitializeAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                await DisconnectAsync();
            }
            try
            {
                var payload = _jwtHandler.GetTokenPayload(token);
                if (payload == null)
                {
                    await DisconnectAsync();
                    
                    return;
                }

                var group = Guid.Parse(payload.Subject).ToUserGroup();
                await Groups.AddToGroupAsync(Context.ConnectionId, group);
                await ConnectAsync(group);
            }
            catch
            {
                await DisconnectAsync();
            }
        }

        private async Task ConnectAsync(string userGroup)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("connected", userGroup);
        }

        private async Task DisconnectAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
        }
    }
}