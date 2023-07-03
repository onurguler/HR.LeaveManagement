using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationCommandValidator(
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;

        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}.");

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveAllocationMustExist)
            .WithMessage("{PropertyName} must be present.");
    }

    private async Task<bool> LeaveAllocationMustExist(int leaveAllocationId, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(leaveAllocationId);
        return leaveAllocation != null;
    }

    private async Task<bool> LeaveTypeMustExist(int leaveTypeId, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(leaveTypeId);
        return leaveType != null;
    }
}