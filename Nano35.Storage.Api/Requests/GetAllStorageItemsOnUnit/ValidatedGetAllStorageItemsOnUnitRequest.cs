using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemsOnUnit
{
    public class GetAllStorageItemsOnUnitValidatorErrorResult : 
        IGetAllStorageItemsOnUnitErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllStorageItemsOnUnitRequest:
        PipeNodeBase<
            IGetAllStorageItemsOnUnitContract, 
            IGetAllStorageItemsOnUnitResultContract>
    {

        public ValidatedGetAllStorageItemsOnUnitRequest(
            IPipeNode<IGetAllStorageItemsOnUnitContract,
                IGetAllStorageItemsOnUnitResultContract> next) : base(next)
        {}

        public override async Task<IGetAllStorageItemsOnUnitResultContract> Ask(
            IGetAllStorageItemsOnUnitContract input)
        {
            if (false)
            {
                return new GetAllStorageItemsOnUnitValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}