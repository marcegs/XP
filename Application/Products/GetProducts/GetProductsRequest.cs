using FluentResults;
using MediatR;

namespace Application.Products.GetProducts;

public class GetProductsRequest : IRequest<Result<GetProductsResponse>>
{
}
