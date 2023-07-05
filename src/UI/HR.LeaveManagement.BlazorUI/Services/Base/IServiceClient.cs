namespace HR.LeaveManagement.BlazorUI.Services.Base;

public partial interface IServiceClient
{
    HttpClient HttpClient { get; }
}