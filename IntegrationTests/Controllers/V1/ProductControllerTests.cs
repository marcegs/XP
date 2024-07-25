using System.Net.Http.Json;
using Application.Products.CreateProduct;
using Application.Products.GetProducts;

namespace IntegrationTests.Controllers.V1;

public class ProductControllerTests
{
    private readonly HttpClient _client;
    private readonly XpAppFactory _app;

    public ProductControllerTests()
    {
        _app = new XpAppFactory();
        _client = _app.CreateClient();
    }

    [Fact]
    public async Task CreateAndGetProducts()
    {
        var firstGetResponse = await _client.GetAsync("api/v1/Product");
        firstGetResponse.EnsureSuccessStatusCode();
        var firstGetProducts = await firstGetResponse.Content.ReadFromJsonAsync<GetProductsResponse>();

        Assert.NotNull(firstGetProducts);
        Assert.Empty(firstGetProducts.Products);

        var request = new CreateProductRequest
        {
            Name = "Test Product",
            ExpirationDate = DateTime.Now.AddYears(1)
        };
        var body = JsonContent.Create(request);

        var createResponse = await _client.PostAsync("api/v1/Product", body);
        createResponse.EnsureSuccessStatusCode();
        
        var secondGetResponse = await _client.GetAsync("api/v1/Product");
        secondGetResponse.EnsureSuccessStatusCode();
        
        var secondGetProducts = await secondGetResponse.Content.ReadFromJsonAsync<GetProductsResponse>();
        
        Assert.NotNull(secondGetProducts);
        Assert.NotEmpty(secondGetProducts.Products);
        Assert.Single(secondGetProducts.Products);
    }
}