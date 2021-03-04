using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetCancellationDetailsById
{
    public class GetCancellationDetailsByIdValidatorErrorResult :
        IGetCancellationDetailsByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetCancellationDetailsByIdRequest:
        IPipelineNode<
            IGetCancellationDetailsByIdRequestContract, 
            IGetCancellationDetailsByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetCancellationDetailsByIdRequestContract, 
            IGetCancellationDetailsByIdResultContract> _nextNode;

        public ValidatedGetCancellationDetailsByIdRequest(
            IPipelineNode<
                IGetCancellationDetailsByIdRequestContract,
                IGetCancellationDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetCancellationDetailsByIdResultContract> Ask(
            IGetCancellationDetailsByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetCancellationDetailsByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}