using FluentValidation;

namespace Nano35.Storage.Api.Requests.GetAllStorageItems
{
    public class GetAllStorageItemsValidator : 
        AbstractValidator<GetAllStorageItemsQuery>
    {
        public GetAllStorageItemsValidator()
        {
            RuleFor(c => c.InstanceId).NotEmpty();
        }
    }
}