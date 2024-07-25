using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsRequest, Result<GetProductsResponse>>
{
    private readonly IXpDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsHandler(IXpDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<GetProductsResponse>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .Where(product => !product.Deleted)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new GetProductsResponse
        {
            count = products.Count,
            Products = _mapper.Map<List<ProductDTO>>(products)
        };
    }
}
