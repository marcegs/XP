using FluentResults;
using MediatR;

namespace Application.Products.UpdateProduct;

public class UpdateProductRequest : IRequest<Result<UpdateProductResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ExpirationDate { get; set; }
}
