using FluentValidation;

namespace Nano35.Storage.Api.Requests.GetAllArticleCategories
{
    public class GetAllArticlesCategoriesValidator : 
        AbstractValidator<GetAllArticlesCategoriesQuery>
    {
        public GetAllArticlesCategoriesValidator()
        {
            RuleFor(c => c.ParentId).NotEmpty();
            RuleFor(c => c.InstanceId).NotEmpty();
        }
    }
}