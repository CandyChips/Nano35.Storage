using FluentValidation;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemConditions
{
    public class GetAllStorageItemConditionsValidator : 
        AbstractValidator<GetAllStorageItemConditionsQuery>
    {
        public GetAllStorageItemConditionsValidator()
        {
        }
    }
}