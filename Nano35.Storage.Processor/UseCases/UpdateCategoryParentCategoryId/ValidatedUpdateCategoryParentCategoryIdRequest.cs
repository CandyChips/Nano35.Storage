using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryParentCategoryId
{
    public class UpdateCategoryParentCategoryIdValidatorErrorResult : 
        IUpdateCategoryParentCategoryIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateCategoryParentCategoryIdRequest:
        PipeNodeBase<
            IUpdateCategoryParentCategoryIdRequestContract,
            IUpdateCategoryParentCategoryIdResultContract>
    {
        public ValidatedUpdateCategoryParentCategoryIdRequest(
            IPipeNode<IUpdateCategoryParentCategoryIdRequestContract,
                IUpdateCategoryParentCategoryIdResultContract> next) : base(next)
        { }

        public override async Task<IUpdateCategoryParentCategoryIdResultContract> Ask(
            IUpdateCategoryParentCategoryIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateCategoryParentCategoryIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}