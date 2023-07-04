using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public bool Approved { get; set; }
}