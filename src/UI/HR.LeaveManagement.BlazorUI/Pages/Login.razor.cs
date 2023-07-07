using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models;

using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages;

public partial class Login
{
    private LoginVM Model { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private string Message { get; set; } = string.Empty;

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; } = null!;
    

    protected override void OnInitialized()
    {
        Model = new LoginVM();
    }

    private async Task HandleLogin()
    {
        if (await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
        {
            NavigationManager.NavigateTo("/");
        }

        Message = "Username/password combination unknown";
    }
}