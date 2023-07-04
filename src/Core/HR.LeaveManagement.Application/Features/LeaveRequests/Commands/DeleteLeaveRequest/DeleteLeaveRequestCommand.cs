using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.DeleteLeaveRequest;

public record DeleteLeaveRequestCommand(int Id) : IRequest;
