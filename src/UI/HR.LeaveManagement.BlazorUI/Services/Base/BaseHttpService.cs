using System.Net;
using System.Net.Http.Headers;

using Blazored.LocalStorage;

namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected IServiceClient _client;
    private readonly ILocalStorageService _localStorageService;

    public BaseHttpService(IServiceClient client, ILocalStorageService localStorageService)
    {
        _client = client;
        _localStorageService = localStorageService;
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

    protected async Task AddBearerToken()
    {
        if (await _localStorageService.ContainKeyAsync("token"))
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer",
                    await _localStorageService.GetItemAsync<string>("token"));
    }
}