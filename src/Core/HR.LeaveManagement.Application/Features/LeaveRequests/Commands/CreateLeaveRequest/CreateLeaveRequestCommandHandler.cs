using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Emails;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Emails;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;

    public CreateLeaveRequestCommandHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender,
        IAppLogger<CreateLeaveRequestCommandHandler> appLogger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Request", validationResult);

        // Get requesting employee's id

        // Check on employee's allocation

        // if allocations aren't enough, return validation error with message

        // Create leave request
        var leaveRequest = _mapper.Map<LeaveRequest>(request);
        await _leaveRequestRepository.CreateAsync(leaveRequest);

        try
        {
            // Send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty, // Get email from employee record
                Body =
                    $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been submitted successfully.",
                Subject = "Leave Request Submitted"
            };

            await _emailSender.SendEmailAsync(email);
        }
        catch (Exception ex)
        {
            _appLogger.LogWarning(ex.Message);
        }

        return leaveRequest.Id;
    }
}