
using System;
using System.Threading.Tasks;
using BeComfy.Common.Authentication;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BeComfy.Services.SignalR.Hubs
{
    public class ClientProcessHub : Hub
    {
        private IJwtHandler _jwtHandler;
        private ILogger _logger;

        public ClientProcessHub(IJwtHandler jwtHandler, ILogger<ClientProcessHub> logger)
        {
            _jwtHandler = jwtHandler;
            _logger = logger;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task Authenticate(string jwt)
        {
            Guid? userId = null;

            if (string.IsNullOrEmpty(jwt))
            {
                _logger.LogInformation($"Hub received empty token");
                await DisconnectUserAsync();
            }

            try
            {
                var jwtToken = _jwtHandler.GetTokenPayload(jwt);
                if (jwtToken == null)
                {
                    await DisconnectUserAsync();
                    return;
                }

                userId = Guid.Parse(jwtToken.Subject);
                var userGroup = userId.Value.ToUserGroup();
                await Groups.AddToGroupAsync(Context.ConnectionId, userGroup);
                await ConnectUserAsync(userId);
            }
            catch
            {
                await DisconnectUserAsync(userId);
            }
        }

        private async Task ConnectUserAsync(Guid? userId)
        {
            _logger.LogInformation($"User: '{userId.Value}' connected to Hub!");
            await Clients.Client(Context.ConnectionId)
                .SendAsync("connected");
        }

        private async Task DisconnectUserAsync(Guid? userId = null)
        {
            if (userId is null)
            {
                _logger.LogInformation($"User disconnected from Hub!");
            }
            else
            {
                _logger.LogInformation($"User '{userId.Value}' disconnected from Hub!");
            }
            await Clients.Client(Context.ConnectionId)
                .SendAsync("disconnected");
        }
    }
}