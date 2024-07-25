using System.Net.Mail;

namespace Application.Common.Interfaces;

public interface ISmtpServer
{
    Task SendMailAsync(string from, string to, string subject, string body);
}