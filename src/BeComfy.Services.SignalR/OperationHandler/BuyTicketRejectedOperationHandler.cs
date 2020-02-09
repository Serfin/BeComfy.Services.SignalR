using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Services.SignalR.HubServices;
using BeComfy.Services.SignalR.Operations;
using BeComfy.Services.SignalR.Operations.Messages.Tickets;

namespace BeComfy.Services.SignalR.OperationHandler
{
    public class BuyTicketRejectedOperationHandler : IEventHandler<BuyTicketRejected>
    {
        private readonly IHubMessageManager _hubMessageManager;

        public BuyTicketRejectedOperationHandler(IHubMessageManager hubMessageManager)
        {
            _hubMessageManager = hubMessageManager;
        }
        public async Task HandleAsync(BuyTicketRejected @event, ICorrelationContext context)
        {
            await _hubMessageManager.PublishOperationSuccessResult(@event.CustomerId,
                OperationStatus.operationRejected,
                new OperationResult 
                {
                    Id = context.Id.ToString(),
                    UserId = context.UserId.ToString(),
                    Code = null,
                    Reason = null,
                    Name = "buy_ticket_rejected",
                }, @event);        
        }
    }
}