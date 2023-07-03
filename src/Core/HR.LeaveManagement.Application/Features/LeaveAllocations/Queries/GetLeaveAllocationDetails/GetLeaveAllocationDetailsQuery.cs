using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;

public record GetLeaveAllocationDetailsQuery(int Id) : IRequest<LeaveAllocationDetailsDto>;
