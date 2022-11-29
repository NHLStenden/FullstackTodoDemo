using FluentValidation;
using TodoBackend.Repositories;

namespace TodoBackend.Requests;

public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryRequestValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        RuleFor(x => x.Description).Length(2, 50).Custom(CheckUniqueDescription);
    }

    private void CheckUniqueDescription(string description, ValidationContext<CategoryRequest> validationContext)
    {
        var category = _categoryRepository.FindCategoryByName(description);
        if (category is not null)
        {
            validationContext.AddFailure($"category name: '{description}' is not unique");
        }
    }
}