using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;

public record GetLeaveAllocationsQuery : IRequest<List<LeaveAllocationDto>>;
