using Blazored.LocalStorage;

using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;

using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(IServiceClient client,
        ILocalStorageService localStorageService,
        AuthenticationStateProvider authenticationStateProvider) : base(client, localStorageService)
    {
        _localStorageService = localStorageService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            AuthRequest authenticationRequest = new() { Email = email, Password = password };
            AuthResponse authenticationResponse = await _client.LoginAsync(authenticationRequest);

            if (string.IsNullOrEmpty(authenticationResponse.Token))
                return false;

            await _localStorageService.SetItemAsync("token", authenticationResponse.Token);

            // Set claims in Blazor and login state
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email,
        string password)
    {
        RegistrationRequest registrationRequest = new()
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = password,
            UserName = userName
        };

        var response = await _client.RegisterAsync(registrationRequest);

        if (string.IsNullOrWhiteSpace(response.UserId))
            return false;

        return true;
    }

    public async Task LogoutAsync()
    {
        await _localStorageService.RemoveItemAsync("token");
        // remove claims in Blazor and invalidate login state
        await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
    }
}