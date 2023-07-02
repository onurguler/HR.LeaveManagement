using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist);
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must not be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} cannot exceed 100.")
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1.");

        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type already exists.");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }

    private Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        return _leaveTypeRepository.IsLeaveTypeNameUnique(command.Name);
    }
}