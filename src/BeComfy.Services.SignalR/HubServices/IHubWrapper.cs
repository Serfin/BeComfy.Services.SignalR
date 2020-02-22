using System;
using System.Threading.Tasks;
using BeComfy.Services.SignalR.Operations;

namespace BeComfy.Services.SignalR.HubServices
{
    public interface IHubWrapper
    {
        Task PublishToUser(Guid userId, string messageName, IOperationResult operationResult);
    }
}