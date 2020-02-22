namespace BeComfy.Services.SignalR.Operations
{
    public class OperationResult : IOperationResult
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Reason { get; set; }
        public object Data { get; set; }
    }
}