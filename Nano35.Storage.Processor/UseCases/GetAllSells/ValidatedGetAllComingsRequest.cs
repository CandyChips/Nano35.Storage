using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllSells
{
    public class GetAllSellsValidatorErrorResult :
        IGetAllSellsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllSellsRequest:
        PipeNodeBase<
            IGetAllSellsRequestContract, 
            IGetAllSellsResultContract>
    {

        public ValidatedGetAllSellsRequest(
            IPipeNode<IGetAllSellsRequestContract, 
                IGetAllSellsResultContract> next) : base(next)
        { }

        public override async Task<IGetAllSellsResultContract> Ask(
            IGetAllSellsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllSellsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}