using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Shared;

public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        
        RuleFor(p => p.StartDate)
            .LessThan(p => p.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

        RuleFor(p => p.EndDate)
            .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");
        
        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");
    }

    private async Task<bool> LeaveTypeMustExist(int leaveTypeId, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(leaveTypeId);
        return leaveType != null;
    }
}