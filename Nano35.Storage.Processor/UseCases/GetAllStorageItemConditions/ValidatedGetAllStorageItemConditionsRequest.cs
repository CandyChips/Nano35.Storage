using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemConditions
{
    public class ValidatedGetAllStorageItemConditionsRequest:
        PipeNodeBase<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>
    {
        public ValidatedGetAllStorageItemConditionsRequest(
            IPipeNode<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract> next) :
            base(next) { }


        public override async Task<IGetAllStorageItemConditionsResultContract> Ask(
            IGetAllStorageItemConditionsRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}