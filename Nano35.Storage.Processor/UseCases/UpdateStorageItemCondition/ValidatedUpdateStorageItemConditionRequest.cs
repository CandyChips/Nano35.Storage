using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemCondition
{
    public class UpdateStorageItemConditionValidatorErrorResult : 
        IUpdateStorageItemConditionErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateStorageItemConditionRequest:
        PipeNodeBase<
            IUpdateStorageItemConditionRequestContract,
            IUpdateStorageItemConditionResultContract>
    {
        public ValidatedUpdateStorageItemConditionRequest(
            IPipeNode<IUpdateStorageItemConditionRequestContract,
                IUpdateStorageItemConditionResultContract> next) : base(next)
        { }

        public override async Task<IUpdateStorageItemConditionResultContract> Ask(
            IUpdateStorageItemConditionRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateStorageItemConditionValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}