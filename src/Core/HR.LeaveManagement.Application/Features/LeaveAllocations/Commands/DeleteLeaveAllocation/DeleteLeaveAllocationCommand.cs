using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;

public record DeleteLeaveAllocationCommand(int Id) : IRequest<Unit>;
