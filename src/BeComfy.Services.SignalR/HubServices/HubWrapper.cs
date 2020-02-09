using System;
using System.Threading.Tasks;
using BeComfy.Services.SignalR.Hubs;
using BeComfy.Services.SignalR.Operations;
using Microsoft.AspNetCore.SignalR;

namespace BeComfy.Services.SignalR.HubServices
{
    public class HubWrapper : IHubWrapper
    {
        private readonly IHubContext<ClientProcessHub> _clientProcessHub;
        
        public HubWrapper(IHubContext<ClientProcessHub> clientProcessHub)
        {
            _clientProcessHub = clientProcessHub;
        }

        public Task PublishToUser(Guid userId, string messageName, IOperationResult operationResult, object data)
            => _clientProcessHub.Clients.Group(userId.ToUserGroup()).SendAsync(messageName, operationResult, data);
    }
}