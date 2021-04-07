using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceValidatorErrorResult : 
        IUpdateStorageItemRetailPriceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateStorageItemRetailPriceRequest:
        PipeNodeBase<
            IUpdateStorageItemRetailPriceRequestContract,
            IUpdateStorageItemRetailPriceResultContract>
    {
        public ValidatedUpdateStorageItemRetailPriceRequest(
            IPipeNode<IUpdateStorageItemRetailPriceRequestContract,
                IUpdateStorageItemRetailPriceResultContract> next) : base(next)
        { }

        public override async Task<IUpdateStorageItemRetailPriceResultContract> Ask(
            IUpdateStorageItemRetailPriceRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateStorageItemRetailPriceValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}