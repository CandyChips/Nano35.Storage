using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateArticle
{
    public class CreateArticleValidatorErrorResult :
        ICreateArticleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateArticleRequest:
        PipeNodeBase<
            ICreateArticleRequestContract, 
            ICreateArticleResultContract>
    {
        public ValidatedCreateArticleRequest(
            IPipeNode<ICreateArticleRequestContract,
                ICreateArticleResultContract> next) : base(next)
        { }

        public override async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateArticleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}