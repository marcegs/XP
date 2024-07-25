using FluentResults;
using MediatR;

namespace Application.Products.CreateProduct;

public class CreateProductRequest : IRequest<Result<CreateProductResponse>>
{
    public string Name { get; set; }
    public DateTime ExpirationDate { get; set; }
}
