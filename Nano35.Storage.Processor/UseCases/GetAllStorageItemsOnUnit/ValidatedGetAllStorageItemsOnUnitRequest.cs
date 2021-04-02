using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnUnit
{
    public class ValidatedGetAllStorageItemsOnUnitRequest:
        PipeNodeBase<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>
    {
        public ValidatedGetAllStorageItemsOnUnitRequest(
            IPipeNode<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract> next) :
            base(next) { }


        public override async Task<IGetAllStorageItemsOnUnitResultContract> Ask(
            IGetAllStorageItemsOnUnitContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}