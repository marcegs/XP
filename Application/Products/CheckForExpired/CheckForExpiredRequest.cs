using FluentResults;
using MediatR;

namespace Application.Products.CheckForExpired;

public class CheckForExpiredRequest : IRequest<Result<CheckForExpiredResponse>>
{
    public string Subject { get; set; }
    public string Body { get; set; }
}