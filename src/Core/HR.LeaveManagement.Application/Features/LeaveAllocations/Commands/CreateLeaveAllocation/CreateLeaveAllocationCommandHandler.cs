using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;

using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IUserService _userService;

    public CreateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IUserService userService)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _userService = userService;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid LeaveAllocation", validationResult);

        // Get leave type for allocations
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        if (leaveType is null)
            throw new NotFoundException(nameof(Domain.LeaveType), request.LeaveTypeId);

        // Get employees
        var employees = await _userService.GetEmployees();

        // Get period
        var period = DateTime.Now.Year;

        // Assign allocations if an allocation doesn't already exist for period and leave type
        var allocations = new List<LeaveAllocation>();
        foreach (var employeeId in employees.Select(s => s.Id))
        {
            var allocationExist =
                await _leaveAllocationRepository.AllocationExists(employeeId, request.LeaveTypeId, period);
            if (allocationExist)
                continue;

            allocations.Add(new LeaveAllocation
            {
                EmployeeId = employeeId,
                LeaveTypeId = request.LeaveTypeId,
                NumberOfDays = leaveType.DefaultDays,
                Period = period
            });
        }

        if (allocations.Any())
        {
            await _leaveAllocationRepository.AddAllocations(allocations);
        }

        return Unit.Value;
    }
}