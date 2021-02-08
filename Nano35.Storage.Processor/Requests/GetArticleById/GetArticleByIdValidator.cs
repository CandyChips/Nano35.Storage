﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllArticleBrands
{
    public class GetArticleByIdValidatorErrorResult :
        IGetArticleByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetArticleByIdValidator:
        IPipelineNode<
            IGetArticleByIdRequestContract, 
            IGetArticleByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetArticleByIdRequestContract, 
            IGetArticleByIdResultContract> _nextNode;

        public GetArticleByIdValidator(
            IPipelineNode<
                IGetArticleByIdRequestContract,
                IGetArticleByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetArticleByIdResultContract> Ask(
            IGetArticleByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetArticleByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}