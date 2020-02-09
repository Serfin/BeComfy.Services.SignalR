using System;
using System.Threading.Tasks;
using BeComfy.Services.SignalR.Operations;

namespace BeComfy.Services.SignalR.HubServices
{
    public class HubMessageManager : IHubMessageManager
    {
        private readonly IHubWrapper _hubWrapper;

        public HubMessageManager(IHubWrapper hubWrapper)
        {
            _hubWrapper = hubWrapper;
        }

        public async Task PublishOperationSuccessResult(Guid userId, string message, IOperationResult operationResult, object data)
            => await _hubWrapper.PublishToUser(userId, message, operationResult, data);

        public async Task PublishOperationRejectedResult(Guid userId, string message,  IOperationResult operationResult, object data)
            => await _hubWrapper.PublishToUser(userId, message, operationResult, data);
    }
}