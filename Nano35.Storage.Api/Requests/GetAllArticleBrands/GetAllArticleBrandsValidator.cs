using FluentValidation;

namespace Nano35.Storage.Api.Requests.GetAllArticleBrands
{
    public class GetAllArticleBrandsValidator : 
        AbstractValidator<GetAllArticleBrandsQuery>
    {
        public GetAllArticleBrandsValidator()
        {
            RuleFor(c => c.CategoryId).NotEmpty();
            RuleFor(c => c.InstanceId).NotEmpty();
        }
    }
}