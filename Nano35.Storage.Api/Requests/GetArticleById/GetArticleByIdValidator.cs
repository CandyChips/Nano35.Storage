using FluentValidation;

namespace Nano35.Storage.Api.Requests.GetArticleById
{
    public class GetArticleByIdValidator : 
        AbstractValidator<GetArticleByIdQuery>
    {
        public GetArticleByIdValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}