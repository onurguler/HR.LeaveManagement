using HR.LeaveManagement.Application.Models.Emails;

namespace HR.LeaveManagement.Application.Contracts.Emails;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(EmailMessage emailMessage);
}