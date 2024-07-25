using Application.Common.DTOs;

namespace Application.Products.GetProducts;

public class GetProductsResponse
{
    public int count { get; set; }
    public List<ProductDTO> Products { get; set; }
}