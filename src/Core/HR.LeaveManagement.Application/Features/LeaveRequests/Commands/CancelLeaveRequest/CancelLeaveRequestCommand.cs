using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;

public record CancelLeaveRequestCommand(int Id) : IRequest<Unit>;