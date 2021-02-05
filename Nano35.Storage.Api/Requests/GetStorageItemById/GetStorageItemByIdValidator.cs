using FluentValidation;

namespace Nano35.Storage.Api.Requests.GetStorageItemById
{
    public class GetStorageItemByIdValidator : 
        AbstractValidator<GetStorageItemByIdQuery>
    {
        public GetStorageItemByIdValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}