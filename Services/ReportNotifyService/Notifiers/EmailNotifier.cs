using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Configuration;
using NetworkConsistency.Domain.Aggregates.FailureReport;

namespace NetworkConsistency.Services.ReportService.Notifiers
{
    public class EmailNotifier : INotifier
    {
        private const string SUBJECT = "Found failure reports";
        private const string NOTIFIER_PARAMETER_PATH = "NotifierParameters:EmailNotifier";
        private const string EMAIL_SEPARATOR = ",";
        
        private const int SSL_PORT = 587;
        private string Host { get; }
        private int Port { get; }
        private string From { get; }
        private string Login { get; }
        private string Pswd { get; }
        private string[] TargetEmails { get; }

        public EmailNotifier(IConfiguration configuration)
        {
            Host = configuration.GetValue<string>($"{NOTIFIER_PARAMETER_PATH}:{nameof(Host)}");
            Port = configuration.GetValue<int>($"{NOTIFIER_PARAMETER_PATH}:{nameof(Port)}");
            From = configuration.GetValue<string>($"{NOTIFIER_PARAMETER_PATH}:{nameof(From)}");
            Login = configuration.GetValue<string>($"{NOTIFIER_PARAMETER_PATH}:{nameof(Login)}");
            Pswd = configuration.GetValue<string>($"{NOTIFIER_PARAMETER_PATH}:{nameof(Pswd)}");
            var targetEmails = configuration.GetValue<string>($"{NOTIFIER_PARAMETER_PATH}:{nameof(TargetEmails)}");
            TargetEmails = targetEmails.Split(EMAIL_SEPARATOR);
        }

        public async Task<Result> NotifyUsers(FailureReport[] report)
        {
            if (report.Length == 0) return Result.Ok();
            
            using var client = new SmtpClient(Host, Port);
            if (!string.IsNullOrWhiteSpace(Login))
            {
                client.Credentials = new NetworkCredential(Login, Pswd);
            }
            if (Port == SSL_PORT)
            {
                client.EnableSsl = true;
            }
            
            var message = $"Found {report.Length} failure reports. Please respond.";
            foreach (var targetEmail in TargetEmails)
            {
                var emailMessage = new MailMessage(From, targetEmail, SUBJECT, message);
                await client.SendMailAsync(emailMessage);
            }
            
            return Result.Ok();
        }
    }
}