using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Pages;

public partial class Index
{
    [Inject]
    AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    [Inject]
    IAuthenticationService AuthenticationService { get; set; } = null!;

    [Inject]
    NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();
    }

    void GoToLogin()
    {
        NavigationManager.NavigateTo("login/");
    }

    void GoToRegister()
    {
        NavigationManager.NavigateTo("register/");
    }

    async Task Logout()
    {
        await AuthenticationService.LogoutAsync();
    }
}