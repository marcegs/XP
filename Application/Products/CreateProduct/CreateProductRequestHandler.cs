using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Products.CreateProduct;

public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Result<CreateProductResponse>>
{
    private readonly IXpDbContext _context;    
    private readonly IMapper _mapper;


    public CreateProductRequestHandler(IXpDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            ExpirationDate = request.ExpirationDate,
        };
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProductResponse { Product = _mapper.Map<ProductDTO>(product) };
    }
}
