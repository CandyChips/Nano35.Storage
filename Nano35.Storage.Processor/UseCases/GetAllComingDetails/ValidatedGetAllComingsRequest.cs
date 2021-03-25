using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllComingDetails
{
    public class GetAllComingDetailsValidatorErrorResult :
        IGetAllComingDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllComingDetailsRequest:
        IPipelineNode<
            IGetAllComingDetailsRequestContract, 
            IGetAllComingDetailsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllComingDetailsRequestContract, 
            IGetAllComingDetailsResultContract> _nextNode;

        public ValidatedGetAllComingDetailsRequest(
            IPipelineNode<
                IGetAllComingDetailsRequestContract,
                IGetAllComingDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllComingDetailsResultContract> Ask(
            IGetAllComingDetailsRequestContract input,
            CancellationToken cancellationToken)
        {
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}