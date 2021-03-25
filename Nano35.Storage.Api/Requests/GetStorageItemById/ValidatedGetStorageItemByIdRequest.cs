using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetStorageItemById
{
    public class GetStorageItemByIdValidatorErrorResult : 
        IGetStorageItemByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetStorageItemByIdRequest:
        PipeNodeBase<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract>
    {
        public ValidatedGetStorageItemByIdRequest(
            IPipeNode<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract> next) :
            base(next) { }

        public override async Task<IGetStorageItemByIdResultContract> Ask(
            IGetStorageItemByIdRequestContract input)
        {
            return await DoNext(input);
        }
    }
}