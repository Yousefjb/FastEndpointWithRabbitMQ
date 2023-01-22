using CatalogService.Services;

public class UpdateProductEndpoint : Endpoint<Product>
{
  public CatalogContext _catalogContext { get; set; }
  public UpdateProductEndpoint(CatalogContext catalogContext)
  {
    _catalogContext = catalogContext;
  }

  public override void Configure()
  {
    Post("/api/catalog/{Id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(Product req, CancellationToken ct)
  {
    var product = await _catalogContext.Products.FindAsync(req.Id);
    if (product == null)
      await SendNotFoundAsync();
    else
    {
      _catalogContext.Entry(product).CurrentValues.SetValues(req);
      await _catalogContext.SaveChangesAsync();
      await SendAsync(req);
    }
  }
}