using FluentValidation;

public class Product
{
  public int Id { get; set; }
  public string Name { get; set; }
  public double Price { get; set; }
  public double Cost { get; set; }
  public string Image64 { get; set; }
}

public class ProductValidator : Validator<Product>
{
  public ProductValidator()
  {
    RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
    .MinimumLength(3).WithMessage("your name is too short!"); ;
  }
}