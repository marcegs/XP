using Application.Common.Errors;
using Application.Common.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Products.DeleteProduct;


public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, Result<DeleteProductResponse>>
{
    private readonly IXpDbContext _context;
    private readonly ILogger<DeleteProductRequestHandler> _logger;

    public DeleteProductRequestHandler(IXpDbContext context, ILogger<DeleteProductRequestHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<DeleteProductResponse>> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.Where(product => product.Id == request.Id && !product.Deleted).FirstOrDefaultAsync(cancellationToken);
        if (product == null)
        {
            _logger.LogWarning("Product with id {request.Id} not found", request.Id);
            var error = new EntityNotFoundError($"Product with id {request.Id} not found");
            return Result.Fail(error);
        }

        product.Deleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteProductResponse();
    }
}