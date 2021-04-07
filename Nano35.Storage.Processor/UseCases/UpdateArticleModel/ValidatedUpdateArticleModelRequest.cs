using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleModel
{
    public class UpdateArticleModelValidatorErrorResult : 
        IUpdateArticleModelErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateArticleModelRequest:
        PipeNodeBase<
            IUpdateArticleModelRequestContract,
            IUpdateArticleModelResultContract>
    {
        public ValidatedUpdateArticleModelRequest(
            IPipeNode<IUpdateArticleModelRequestContract,
                IUpdateArticleModelResultContract> next) : base(next)
        { }

        public override async Task<IUpdateArticleModelResultContract> Ask(
            IUpdateArticleModelRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateArticleModelValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}