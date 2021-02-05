using FluentValidation;

namespace Nano35.Storage.Api.Requests.CreateCategory
{
    public class CreateCategoryCommandValidation :
        AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidation()
        {
            RuleFor(c => c.NewId).NotEmpty();
            RuleFor(c => c.InstanceId).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.ParentCategoryId).NotEmpty();
        }
    }
}