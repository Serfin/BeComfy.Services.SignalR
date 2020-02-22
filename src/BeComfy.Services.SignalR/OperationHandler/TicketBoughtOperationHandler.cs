using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Services.SignalR.HubServices;
using BeComfy.Services.SignalR.Operations;
using BeComfy.Services.SignalR.Operations.Messages.Tickets;

namespace BeComfy.Services.SignalR.OperationHandler
{
    public class TicketBoughtOperationsHandler : IEventHandler<TicketBought>
    {
        private readonly IHubMessageManager _hubMessageManager;

        public TicketBoughtOperationsHandler(IHubMessageManager hubMessageManager)
        {
            _hubMessageManager = hubMessageManager;
        }

        public async Task HandleAsync(TicketBought @event, ICorrelationContext context)
        {
            await _hubMessageManager.PublishOperationSuccessResult(@event.CustomerId,
                OperationStatus.operationSuccess,
                new OperationResult 
                {
                    Id = context.Id.ToString(),
                    UserId = context.UserId.ToString(),
                    Code = null,
                    Reason = null,
                    Name = "ticket_bought",
                    Data = @event
                });
        }
    }
}