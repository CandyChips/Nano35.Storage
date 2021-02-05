using FluentValidation;

namespace Nano35.Storage.Api.Requests.GetAllArticles
{
    public class GetAllArticlesValidator : 
        AbstractValidator<GetAllArticlesQuery>
    {
        public GetAllArticlesValidator()
        {
            RuleFor(c => c.InstanceId).NotEmpty();
        }
    }
}