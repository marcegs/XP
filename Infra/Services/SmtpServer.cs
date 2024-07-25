using System.Net.Mail;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infra.Services;

public class SmtpServer : ISmtpServer
{
    private readonly SmtpClient _client;
    private readonly ILogger<SmtpServer> _logger;

    public SmtpServer(ILogger<SmtpServer> logger)
    {
        _logger = logger;
        _client = new SmtpClient
        {
            Port = 25,
            Host = "smtp",
        };
    }
  
    public async Task SendMailAsync(string from, string to, string subject, string body)
    {
        try
        {
            await _client.SendMailAsync(from, to, subject, body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}