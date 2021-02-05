using FluentValidation;

namespace Nano35.Storage.Api.Requests.GetAllArticleModels
{
    public class GetAllArticlesModelValidator : 
        AbstractValidator<GetAllArticlesModelsQuery>
    {
        public GetAllArticlesModelValidator()
        {
            RuleFor(c => c.CategoryId).NotEmpty();
            RuleFor(c => c.InstanceId).NotEmpty();
        }
    }
}