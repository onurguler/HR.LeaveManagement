using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;

using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; } = null!;

    [Inject]
    public ILeaveAllocationService LeaveAllocationService { get; set; } = null!;

    List<LeaveTypeVM>? LeaveTypes { get; set; }
    string? Message { get; set; }

    void CreateLeaveType()
    {
        NavigationManager.NavigateTo("/leave-types/create");
    }

    async Task AllocateLeaveType(int id)
    {
        // Use Leave Allocation Service here
        await LeaveAllocationService.CreateLeaveAllocations(id);
    }

    void DetailsLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leave-types/details/{id}");
    }

    void EditLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leave-types/edit/{id}");
    }

    async Task DeleteLeaveType(int id)
    {
        var response = await LeaveTypeService.DeleteLeaveType(id);
        if (response.Success)
        {
            StateHasChanged();
        }
        else
        {
            Message = response.Message;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        LeaveTypes = await LeaveTypeService.GetLeaveTypes();
    }
}