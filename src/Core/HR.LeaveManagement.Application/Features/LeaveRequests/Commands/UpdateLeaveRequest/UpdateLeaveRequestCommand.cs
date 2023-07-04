using HR.LeaveManagement.Application.Features.LeaveRequests.Shared;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommand : BaseLeaveRequest, IRequest
{
    public int Id { get; set; }
    public string RequestComments { get; set; } = string.Empty;
    public bool Cancelled { get; set; }
}