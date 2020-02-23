using System;
using System.Collections.Generic;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.Messages;
using BeComfy.Common.Types.Enums;
using Newtonsoft.Json;

namespace BeComfy.Services.SignalR.Operations.Messages.Tickets
{
    [MessageNamespace("tickets")] 
    public class TicketBought : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public decimal TotalPrice { get; }
        public IDictionary<SeatClass, int> AvailableSeats { get; }

        [JsonConstructor]
        public TicketBought(Guid id, Guid customerId, decimal totalPrice,
            IDictionary<SeatClass, int> availableSeats)
        {
            Id = id;
            CustomerId = customerId;
            TotalPrice = totalPrice;
            AvailableSeats = availableSeats;
        }
    }
}