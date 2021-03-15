﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleInfo
{
    public class UpdateArticleInfoValidatorErrorResult : 
        IUpdateArticleInfoErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateArticleInfoRequest:
        IPipelineNode<
            IUpdateArticleInfoRequestContract,
            IUpdateArticleInfoResultContract>
    {
        private readonly IPipelineNode<
            IUpdateArticleInfoRequestContract, 
            IUpdateArticleInfoResultContract> _nextNode;

        public ValidatedUpdateArticleInfoRequest(
            IPipelineNode<
                IUpdateArticleInfoRequestContract,
                IUpdateArticleInfoResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateArticleInfoResultContract> Ask(
            IUpdateArticleInfoRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateArticleInfoValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}