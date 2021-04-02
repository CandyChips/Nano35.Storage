using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItems
{
    public class ValidatedGetAllStorageItemsRequest:
        PipeNodeBase<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract>
    {
        public ValidatedGetAllStorageItemsRequest(
            IPipeNode<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllStorageItemsResultContract> Ask(
            IGetAllStorageItemsRequestContract input)
        {
            return await DoNext(input);
        }
    }
}