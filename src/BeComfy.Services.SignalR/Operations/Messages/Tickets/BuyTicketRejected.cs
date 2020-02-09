using System;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.Messages;
using Newtonsoft.Json;

namespace BeComfy.Services.SignalR.Operations.Messages.Tickets
{
    [MessageNamespace("tickets")] 
    public class BuyTicketRejected : IEvent
    {
        public Guid TicketId { get; }
        public Guid CustomerId { get; set; }
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public BuyTicketRejected(Guid ticketId, Guid customerId, string code, string reason)
        {
            TicketId = ticketId;
            CustomerId = customerId;
            Code = code;
            Reason = reason;
        }
    }
}