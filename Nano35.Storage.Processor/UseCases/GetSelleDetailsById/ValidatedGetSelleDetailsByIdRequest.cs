using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetSelleDetailsById
{
    public class GetSelleDetailsByIdValidatorErrorResult :
        IGetSelleDetailsByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetSelleDetailsByIdRequest:
        IPipelineNode<
            IGetSelleDetailsByIdRequestContract, 
            IGetSelleDetailsByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetSelleDetailsByIdRequestContract, 
            IGetSelleDetailsByIdResultContract> _nextNode;

        public ValidatedGetSelleDetailsByIdRequest(
            IPipelineNode<
                IGetSelleDetailsByIdRequestContract,
                IGetSelleDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetSelleDetailsByIdResultContract> Ask(
            IGetSelleDetailsByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetSelleDetailsByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}