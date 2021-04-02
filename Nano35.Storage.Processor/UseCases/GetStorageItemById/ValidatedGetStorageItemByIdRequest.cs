using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetStorageItemById
{
    public class ValidatedGetStorageItemByIdRequest:
        PipeNodeBase<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract>
    {
        public ValidatedGetStorageItemByIdRequest(
            IPipeNode<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract> next) :
            base(next) { }

        public override async Task<IGetStorageItemByIdResultContract> Ask(
            IGetStorageItemByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}