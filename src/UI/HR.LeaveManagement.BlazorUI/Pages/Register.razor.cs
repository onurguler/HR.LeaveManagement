using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models;

using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages;

public partial class Register
{
    public RegisterVM Model { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    public string Message { get; set; } = string.Empty;

    [Inject]
    public IAuthenticationService AuthenticationService { get; set; } = null!;

    protected override void OnInitialized()
    {
        Model = new RegisterVM();
    }

    protected async Task HandleRegister()
    {
        var result = await AuthenticationService.RegisterAsync(
            Model.FirstName,
            Model.LastName,
            Model.UserName,
            Model.Email,
            Model.Password);

        if (result)
        {
            NavigationManager.NavigateTo("/");
        }

        Message = "Something went wrong. Please try again.";
    }
}