using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellationDetails
{
    public class GetAllCancellationDetailsValidatorErrorResult :
        IGetAllCancellationDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllCancellationDetailsRequest:
        IPipelineNode<
            IGetAllCancellationDetailsRequestContract, 
            IGetAllCancellationDetailsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllCancellationDetailsRequestContract, 
            IGetAllCancellationDetailsResultContract> _nextNode;

        public ValidatedGetAllCancellationDetailsRequest(
            IPipelineNode<
                IGetAllCancellationDetailsRequestContract,
                IGetAllCancellationDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllCancellationDetailsResultContract> Ask(
            IGetAllCancellationDetailsRequestContract input,
            CancellationToken cancellationToken)
        {
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}