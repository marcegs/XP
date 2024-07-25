using System.Net.Mail;
using Application.Common.Interfaces;
using Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Products.CheckForExpired;

public class CheckForExpiredRequestHandler : IRequestHandler<CheckForExpiredRequest, Result<CheckForExpiredResponse>>
{
    private readonly ISmtpServer _smtpServer;
    private readonly IXpDbContext _context;
    private readonly ILogger<CheckForExpiredRequestHandler> _logger;

    public CheckForExpiredRequestHandler(ISmtpServer smtpServer, IXpDbContext context, ILogger<CheckForExpiredRequestHandler> logger)
    {
        _smtpServer = smtpServer;
        _context = context;
        _logger = logger;
    }

    public async Task<Result<CheckForExpiredResponse>> Handle(CheckForExpiredRequest request, CancellationToken cancellationToken)
    {
        var closeToExpiredProducts = await _context.Products.AsNoTracking()
            .Where(prod => !prod.Deleted 
                           && prod.ExpirationDate < DateTime.UtcNow.AddDays(10)
                           && DateTime.UtcNow < prod.ExpirationDate)
            .ToListAsync(cancellationToken);
        
        if (closeToExpiredProducts.Count == 0) return Result.Ok();
        var subject = $"Attention required, {closeToExpiredProducts.Count} products close to expire";
        var body = $"Our system has found {closeToExpiredProducts.Count} products that will expire on the next 10 days.\n" +
                   $"Bellow are all products close to expire:\n" +
                   $"\n" +
                   $"\n" +
                   $"ID / NAME / EXPIRATION DATE\n";
        foreach (var product in closeToExpiredProducts) body += $"{product.Id} / {product.Name} / {product.ExpirationDate}\n";

        await _smtpServer.SendMailAsync("server@email", "recipient@email", subject, body);

        return new CheckForExpiredResponse();
    }
}