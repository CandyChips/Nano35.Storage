using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateStorageItem
{
    public class ValidatedCreateStorageItemRequest:
        PipeNodeBase<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>
    {
        public ValidatedCreateStorageItemRequest(
            IPipeNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract> next) :
            base(next) { }

        public override async Task<ICreateStorageItemResultContract> Ask(
            ICreateStorageItemRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}