using System.Diagnostics;
using Application.Common.Errors;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Common.Entities;

namespace Web.Controllers;

[ApiController]
public class BaseController<T> : ControllerBase
{
    private IMediator? _mediator;
    private readonly ILogger<T> _logger;

    public BaseController(ILogger<T> logger)
    {
        _logger = logger;
    }

    protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()) ?? throw new InvalidOperationException();

    protected ActionResult<TResponse> HandleResult<TResponse>(Result<TResponse> result)
    {
        if (result.IsSuccess) return Ok(result.Value);
        var traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        if (result.HasError<EntityNotFoundError>())
            return NotFound(new BaseErrorMessage
            {
                traceId = traceId,
                Title = "Not found.",
                Errors = result.Errors.Select(e => e.Message),
                Status = StatusCodes.Status404NotFound
            });

        if (result.HasError<BadRequestError>())
            return NotFound(new BaseErrorMessage
            {
                traceId = traceId,
                Title = "Bad Request.",
                Errors = result.Errors.Select(e => e.Message),
                Status = StatusCodes.Status400BadRequest
            });

        _logger.LogError("Request failed with errors: {Errors}", result.Errors);
        
        return StatusCode(500, new BaseErrorMessage
        {
            traceId = traceId,
            Title = "Internal Server Error.",
            Status = StatusCodes.Status500InternalServerError
        });
    }
}