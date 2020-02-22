using System;
using System.Threading.Tasks;
using BeComfy.Services.SignalR.Operations;

namespace BeComfy.Services.SignalR.HubServices
{
    public interface IHubMessageManager
    {
        Task PublishOperationSuccessResult(Guid userId, string message, IOperationResult operationResult);
        Task PublishOperationRejectedResult(Guid userId, string message, IOperationResult operationResult);
    }
}