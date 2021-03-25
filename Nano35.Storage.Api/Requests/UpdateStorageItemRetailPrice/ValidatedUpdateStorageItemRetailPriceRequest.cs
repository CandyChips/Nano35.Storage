using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceValidatorErrorResult : 
        IUpdateStorageItemRetailPriceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateStorageItemRetailPriceRequest:
        PipeNodeBase<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract>
    {
        public ValidatedUpdateStorageItemRetailPriceRequest(
            IPipeNode<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract> next) :
            base(next) { }

        public override async Task<IUpdateStorageItemRetailPriceResultContract> Ask(
            IUpdateStorageItemRetailPriceRequestContract input)
        {
            return await DoNext(input);
        }
    }
}