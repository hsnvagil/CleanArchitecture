using App.Application.Contracts.Persistence;
using FluentValidation;

namespace App.Application.Features.Products.Create;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest> {
    private readonly IProductRepository _productRepository;

    public CreateProductRequestValidator(IProductRepository productRepository) {
        _productRepository = productRepository;
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Product name is required")
            .NotEmpty().WithMessage("Product name is not empty.")
            .Length(3, 10).WithMessage("Length must be between 3 and 10 characters.")
            .Must(MustUniqueProductName).WithMessage("Product name already exists.");

        RuleFor(x => x.Price)
            .NotNull().WithMessage("Product price is required.")
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");

        RuleFor(x => x.CategoryId)
            .NotNull().WithMessage("Product's category is required")
            .GreaterThan(0).WithMessage("Product's category must be greather than zero.");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stock must be between 1 and 100.");
    }

    private bool MustUniqueProductName(string name) {
        return !_productRepository.Where(x => x.Name == name.ToLower()).Any();
    }
}