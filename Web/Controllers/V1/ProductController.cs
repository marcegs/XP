using Application.Products;
using Application.Products.CheckForExpired;
using Application.Products.CreateProduct;
using Application.Products.DeleteProduct;
using Application.Products.GetProducts;
using Application.Products.UpdateProduct;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.V1;

[Route("api/v1/[controller]")]
public class ProductController : BaseController<ProductController>
{
    public ProductController(ILogger<ProductController> logger) : base(logger)
    {
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<DeleteProductResponse>> DeleteProduct(int id)
    {
        var request = new DeleteProductRequest() { Id = id };
        var result = await Mediator.Send(request);
        
        return HandleResult(result);
    }
    
    [HttpPost("checkExpired")]
    public async Task<ActionResult<CheckForExpiredResponse>> CheckExpired()
    {
        var request = new CheckForExpiredRequest();
        var result = await Mediator.Send(request);
        
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<CreateProductResponse>> CreateProduct(CreateProductRequest request)
    {
        var result = await Mediator.Send(request);
        return HandleResult(result);
    }

    [HttpPatch]
    public async Task<ActionResult<UpdateProductResponse>> UpdateProduct(UpdateProductRequest request)
    {
        var result = await Mediator.Send(request);
        return HandleResult(result);
    }

    [HttpGet]
    public async Task<ActionResult<GetProductsResponse>> GetProducts()
    {
        var result = await Mediator.Send(new GetProductsRequest());
        return HandleResult(result);
    }
}