using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllWarehouseOfStorageItem
{
    public class GetAllWarehouseOfStorageItemValidatorErrorResult : 
        IGetAllWarehouseOfStorageItemErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllWarehouseOfStorageItemRequest:
        PipeNodeBase<
            IGetAllWarehouseOfStorageItemRequestContract, 
            IGetAllWarehouseOfStorageItemResultContract>
    {

        public ValidatedGetAllWarehouseOfStorageItemRequest(
            IPipeNode<IGetAllWarehouseOfStorageItemRequestContract,
                IGetAllWarehouseOfStorageItemResultContract> next) : base(next)
        {}

        public override async Task<IGetAllWarehouseOfStorageItemResultContract> Ask(
            IGetAllWarehouseOfStorageItemRequestContract input)
        {
            if (false)
            {
                return new GetAllWarehouseOfStorageItemValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}