﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleCategories
{
    public class GetAllArticlesCategoriesValidatorErrorResult :
        IGetAllArticlesCategoriesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllArticlesCategoriesRequest:
        IPipelineNode<
            IGetAllArticlesCategoriesRequestContract, 
            IGetAllArticlesCategoriesResultContract>
    {
        private readonly IPipelineNode<
            IGetAllArticlesCategoriesRequestContract, 
            IGetAllArticlesCategoriesResultContract> _nextNode;

        public ValidatedGetAllArticlesCategoriesRequest(
            IPipelineNode<
                IGetAllArticlesCategoriesRequestContract,
                IGetAllArticlesCategoriesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllArticlesCategoriesResultContract> Ask(
            IGetAllArticlesCategoriesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllArticlesCategoriesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}