using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetComingDetailsById
{
    public class GetComingDetailsByIdValidatorErrorResult :
        IGetComingDetailsByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetComingDetailsByIdRequest:
        IPipelineNode<
            IGetComingDetailsByIdRequestContract, 
            IGetComingDetailsByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetComingDetailsByIdRequestContract, 
            IGetComingDetailsByIdResultContract> _nextNode;

        public ValidatedGetComingDetailsByIdRequest(
            IPipelineNode<
                IGetComingDetailsByIdRequestContract,
                IGetComingDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetComingDetailsByIdResultContract> Ask(
            IGetComingDetailsByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetComingDetailsByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}