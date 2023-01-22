using CatalogService.Services;

public class DeleteProductEndpoint : EndpointWithoutRequest
{
  public CatalogContext _catalogContext { get; set; }
  public DeleteProductEndpoint(CatalogContext catalogContext)
  {
    _catalogContext = catalogContext;
  }

  public override void Configure()
  {
    Delete("/api/catalog/{Id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var Id = Route<int>("Id");
    var product = await _catalogContext.Products.FindAsync(Id);
    if (product == null)
      await SendNotFoundAsync();
    else
    {
      _catalogContext.Remove(product);
      await _catalogContext.SaveChangesAsync();
      await SendOkAsync();
    }
  }
}