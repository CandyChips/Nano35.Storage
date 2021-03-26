using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllSelleDetails
{
    public class GetAllSelleDetailsValidatorErrorResult :
        IGetAllSelleDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllSelleDetailsRequest:
        IPipelineNode<
            IGetAllSelleDetailsRequestContract, 
            IGetAllSelleDetailsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllSelleDetailsRequestContract, 
            IGetAllSelleDetailsResultContract> _nextNode;

        public ValidatedGetAllSelleDetailsRequest(
            IPipelineNode<
                IGetAllSelleDetailsRequestContract,
                IGetAllSelleDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllSelleDetailsResultContract> Ask(
            IGetAllSelleDetailsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllSelleDetailsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}