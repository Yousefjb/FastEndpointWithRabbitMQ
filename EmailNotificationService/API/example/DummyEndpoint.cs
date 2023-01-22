public class DummyEndpoint : EndpointWithoutRequest
{
  public override void Configure()
  {
    Get("/api/example/dummy");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    await SendAsync("Hello World");
  }
}