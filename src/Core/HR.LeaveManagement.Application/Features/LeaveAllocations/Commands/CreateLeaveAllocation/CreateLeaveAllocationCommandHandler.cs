using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationCommandHandler(
        IMapper mapper,
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid LeaveAllocation", validationResult);

        // Get leave type for allocations
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        // Get employees

        // Get period

        // Assign allocations
        var leaveAllocation = _mapper.Map<LeaveAllocation>(request);
        await _leaveAllocationRepository.CreateAsync(leaveAllocation);
        return leaveAllocation.Id;
    }
}