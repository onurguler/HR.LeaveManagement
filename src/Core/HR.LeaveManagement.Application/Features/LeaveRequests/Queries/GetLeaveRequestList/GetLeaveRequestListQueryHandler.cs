using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request,
        CancellationToken cancellationToken)
    {
        // Check if it is logged in employee

        var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
        var requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

        // Fill requests with employee information

        return requests;
    }
}