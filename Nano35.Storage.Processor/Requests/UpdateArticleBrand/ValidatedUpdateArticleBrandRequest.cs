using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.UpdateArticleBrand
{
    public class UpdateArticleBrandValidatorErrorResult : 
        IUpdateArticleBrandErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateArticleBrandRequest:
        IPipelineNode<
            IUpdateArticleBrandRequestContract,
            IUpdateArticleBrandResultContract>
    {
        private readonly IPipelineNode<
            IUpdateArticleBrandRequestContract, 
            IUpdateArticleBrandResultContract> _nextNode;

        public ValidatedUpdateArticleBrandRequest(
            IPipelineNode<
                IUpdateArticleBrandRequestContract,
                IUpdateArticleBrandResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateArticleBrandResultContract> Ask(
            IUpdateArticleBrandRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateArticleBrandValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}