using Microsoft.AspNetCore.Mvc;

namespace Web.Common.Entities;

public class BaseErrorMessage() : ProblemDetails
{
    public string traceId { get; set; }
    public IEnumerable<string> Errors { get; set; }
};