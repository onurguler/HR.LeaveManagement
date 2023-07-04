using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetail;

public record GetLeaveRequestDetailQuery(int Id) : IRequest<LeaveRequestDetailsDto>;
