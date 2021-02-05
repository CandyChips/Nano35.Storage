using FluentValidation;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class CreateArticleCommandValidation : 
        AbstractValidator<CreateArticleCommand>
    {    
        public CreateArticleCommandValidation()
        {
            RuleFor(c => c.Brand).NotEmpty();
            RuleFor(c => c.Model).NotEmpty();
            RuleFor(c => c.NewId).NotEmpty();
            RuleFor(c => c.CategoryId).NotEmpty();
            RuleFor(c => c.InstanceId).NotEmpty();
        }
        
    }
}