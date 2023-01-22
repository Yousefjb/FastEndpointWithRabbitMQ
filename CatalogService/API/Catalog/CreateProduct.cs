using CatalogService.Services;

public class CreateProductEndpoint : Endpoint<Product>
{
  public CatalogContext _catalogContext { get; set; }
  public CatalogMessageQueue _messageQueue { get; set; }
  public CreateProductEndpoint(CatalogContext catalogContext, CatalogMessageQueue messageQueue)
  {
    _catalogContext = catalogContext;
    _messageQueue = messageQueue;
  }

  public override void Configure()
  {
    Post("/api/catalog");
    AllowAnonymous();
  }

  public override async Task HandleAsync(Product req, CancellationToken ct)
  {
    _catalogContext.Products.Add(req);
    await _catalogContext.SaveChangesAsync();
    _messageQueue.PublishProductCreated(req);

    await SendAsync(req);
  }
}