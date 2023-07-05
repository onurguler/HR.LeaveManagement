using System.Net;

namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected IServiceClient _client;

    public BaseHttpService(IServiceClient client)
    {
        _client = client;
    }

    protected Response<Guid> ConvertApiException<Guid>(ApiException ex)
    {
        if (ex.StatusCode == (int)HttpStatusCode.BadRequest)
        {
            return new Response<Guid>()
            {
                Message = "Invalid data was submitted", ValidationErrors = ex.Response, Success = false
            };
        }

        if (ex.StatusCode == (int)HttpStatusCode.NotFound)
        {
            return new Response<Guid>() { Message = "The record was not found.", Success = false, };
        }

        return new Response<Guid>() { Message = "Something went wrong, please try again later.", Success = false };
    }
}