using FluentResults;
using MediatR;

namespace Application.Products.DeleteProduct;

public class DeleteProductRequest : IRequest<Result<DeleteProductResponse>>
{
    public int Id { get; set; }
}