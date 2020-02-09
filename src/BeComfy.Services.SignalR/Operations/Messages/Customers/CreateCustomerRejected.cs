using System;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.Messages;
using Newtonsoft.Json;

namespace BeComfy.Services.SignalR.Operations.Messages.Customers
{
    [MessageNamespace("customers")]
    public class CreateCustomerRejected : IRejectedEvent
    {
        public Guid CustomerId { get; }
        public string Code { get; }
        public string Reason { get; }
        
        [JsonConstructor]
        public CreateCustomerRejected(Guid customerId, string code, string reason)
        {
            CustomerId = customerId;
            Code = code;
            Reason = reason;
        }
    }
}