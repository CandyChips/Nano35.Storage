using FluentValidation;

namespace Nano35.Storage.Api.Requests.CreateStorageItem
{
    public class CreateStorageItemCommandValidator : 
        AbstractValidator<CreateStorageItemCommand>
    {    
        public CreateStorageItemCommandValidator()
        {
            RuleFor(c => c.NewId).NotEmpty();
            RuleFor(c => c.InstanceId).NotEmpty();
            RuleFor(c => c.Comment).NotEmpty();
            RuleFor(c => c.HiddenComment).NotEmpty();
            RuleFor(c => c.RetailPrice).NotEmpty();
            RuleFor(c => c.PurchasePrice).NotEmpty();
            RuleFor(c => c.ArticleId).NotEmpty();
            RuleFor(c => c.ConditionId).NotEmpty();
        }
        
    }
}