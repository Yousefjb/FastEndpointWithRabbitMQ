using CatalogService.Services;

public class GetProductEndpoint : EndpointWithoutRequest
{
  public CatalogContext _catalogContext { get; set; }
  public GetProductEndpoint(CatalogContext catalogContext)
  {
    _catalogContext = catalogContext;
  }

  public override void Configure()
  {
    Get("/api/catalog/{Id?}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var Id = Route<int?>("Id", false);
    if (Id != null)
    {
      var product = await _catalogContext.Products.FindAsync(Id);
      if (product != null)
        await SendAsync(product);
      else
        await SendNotFoundAsync();
    }
    else
    {
      var products = _catalogContext.Products;
      await SendAsync(products);
    }
  }
}