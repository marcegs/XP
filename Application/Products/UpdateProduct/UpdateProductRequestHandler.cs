using Application.Common.DTOs;
using Application.Common.Errors;
using Application.Common.Interfaces;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Products.UpdateProduct;

public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, Result<UpdateProductResponse>>
{
    private readonly IXpDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductRequestHandler> _logger;

    public UpdateProductRequestHandler(IXpDbContext context, IMapper mapper, ILogger<UpdateProductRequestHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<UpdateProductResponse>> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.Where(product => product.Id == request.Id && !product.Deleted).FirstOrDefaultAsync(cancellationToken);
        if (product == null)
        {
            _logger.LogWarning("Product with id {request.Id} not found", request.Id);
            var error = new EntityNotFoundError($"Product with id {request.Id} not found");
            return Result.Fail(error);
        }

        product.Name = request.Name;
        product.ExpirationDate = request.ExpirationDate;
        
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateProductResponse
        {
            Product = _mapper.Map<ProductDTO>(product)
        };
    }
}
