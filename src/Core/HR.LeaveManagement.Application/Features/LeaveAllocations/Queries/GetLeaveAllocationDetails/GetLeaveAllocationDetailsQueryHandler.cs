using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler
    : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocationDetailsQueryHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

        if (leaveAllocation == null)
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);

        return _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
    }
}