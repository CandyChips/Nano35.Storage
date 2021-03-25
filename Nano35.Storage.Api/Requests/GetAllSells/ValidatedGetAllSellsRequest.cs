using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllSells
{
    public class GetAllSellsValidatorErrorResult : 
        IGetAllSellsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllSellsRequest:
        PipeNodeBase<IGetAllSellsRequestContract, IGetAllSellsResultContract>
    {
        public ValidatedGetAllSellsRequest(
            IPipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllSellsResultContract> Ask(
            IGetAllSellsRequestContract input)
        {
            return await DoNext(input);
        }
    }
}