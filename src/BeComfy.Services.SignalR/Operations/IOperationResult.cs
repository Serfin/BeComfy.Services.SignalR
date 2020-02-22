namespace BeComfy.Services.SignalR.Operations
{
    public interface IOperationResult
    {
        string Id { get; set; }
        string UserId { get; set; }
        string Name { get; set; }
        string Code { get; set; }
        string Reason { get; set; }
        object Data { get; set; }
    }
}