using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Services.SignalR.HubServices;
using BeComfy.Services.SignalR.Operations;
using BeComfy.Services.SignalR.Operations.Messages.Customers;

namespace BeComfy.Services.SignalR.OperationHandler
{
    public class CustomerOperationsHandler : IEventHandler<CreateCustomerRejected>
    {
        private readonly IHubMessageManager _hubMessageManager;

        public CustomerOperationsHandler(IHubMessageManager hubMessageManager)
        {
            _hubMessageManager = hubMessageManager;
        }
        public async Task HandleAsync(CreateCustomerRejected @event, ICorrelationContext context)
        {
            await _hubMessageManager.PublishOperationRejectedResult(@event.CustomerId,
                OperationStatus.operationRejected,
                new OperationResult 
                {
                    Id = context.Id.ToString(),
                    UserId = context.UserId.ToString(),
                    Code = null,
                    Reason = null,
                    Name = "create_customer_rejected",
                }, @event);        
        }
    }
}