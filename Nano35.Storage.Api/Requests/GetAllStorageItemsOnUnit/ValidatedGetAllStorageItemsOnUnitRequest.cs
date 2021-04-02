using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemsOnUnit
{
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
            return await DoNext(input);
        }
    }
}