using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemCondition
{
    public class ValidatedUpdateStorageItemConditionRequest:
        PipeNodeBase<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract>
    {
        public ValidatedUpdateStorageItemConditionRequest(
            IPipeNode<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract> next) :
            base(next) { }

        public override async Task<IUpdateStorageItemConditionResultContract> Ask(
            IUpdateStorageItemConditionRequestContract input)
        {
            return await DoNext(input);
        }
    }
}