using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnUnit
{
    public class GetAllPlacesOfStorageItemOnUnitValidatorErrorResult : 
        IGetAllPlacesOfStorageItemOnUnitErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllPlacesOfStorageItemOnUnitRequest:
        PipeNodeBase<
            IGetAllPlacesOfStorageItemOnUnitRequestContract, 
            IGetAllPlacesOfStorageItemOnUnitResultContract>
    {

        public ValidatedGetAllPlacesOfStorageItemOnUnitRequest(
            IPipeNode<IGetAllPlacesOfStorageItemOnUnitRequestContract,
                IGetAllPlacesOfStorageItemOnUnitResultContract> next) : base(next)
        {}

        public override async Task<IGetAllPlacesOfStorageItemOnUnitResultContract> Ask(
            IGetAllPlacesOfStorageItemOnUnitRequestContract input)
        {
            if (false)
            {
                return new GetAllPlacesOfStorageItemOnUnitValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}