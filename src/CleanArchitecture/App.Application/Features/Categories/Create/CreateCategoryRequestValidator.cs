using App.Application.Contracts.Persistence;
using FluentValidation;

namespace App.Application.Features.Categories.Create;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest> {
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryRequestValidator(ICategoryRepository categoryRepository) {
        _categoryRepository = categoryRepository;
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Category name is required")
            .NotEmpty().WithMessage("Category name is not empty.")
            .Length(3, 10).WithMessage("Length must be between 3 and 10 characters.")
            .Must(MustUniqueProductName).WithMessage("Category name already exists.");
    }

    private bool MustUniqueProductName(string name) {
        return !_categoryRepository.Where(x => x.Name == name.ToLower()).Any();
    }
}