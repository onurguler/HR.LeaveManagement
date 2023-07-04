using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public record GetLeaveRequestListQuery : IRequest<List<LeaveRequestListDto>>;
