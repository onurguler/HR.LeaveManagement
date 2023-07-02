using System.Net;
using HR.LeaveManagement.Application.Contracts.Emails;
using HR.LeaveManagement.Application.Models.Emails;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HR.LeaveManagement.Infrastructure.Emails;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettingsOptions)
    {
        _emailSettings = emailSettingsOptions.Value;
    }

    public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);
        var to = new EmailAddress(emailMessage.To);
        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };

        var message = MailHelper.CreateSingleEmail(
            from,
            to,
            emailMessage.Subject,
            emailMessage.Body,
            emailMessage.Body);
        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}