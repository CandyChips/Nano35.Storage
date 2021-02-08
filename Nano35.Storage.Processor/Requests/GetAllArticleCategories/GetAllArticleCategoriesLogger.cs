﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllArticleCategories
{
    public class GetAllArticlesCategoriesLogger :
        IPipelineNode<
            IGetAllArticlesCategoriesRequestContract, 
            IGetAllArticlesCategoriesResultContract>
    {
        private readonly ILogger<GetAllArticlesCategoriesLogger> _logger;
        private readonly IPipelineNode<
            IGetAllArticlesCategoriesRequestContract,
            IGetAllArticlesCategoriesResultContract> _nextNode;

        public GetAllArticlesCategoriesLogger(
            ILogger<GetAllArticlesCategoriesLogger> logger,
            IPipelineNode<
                IGetAllArticlesCategoriesRequestContract,
                IGetAllArticlesCategoriesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllArticlesCategoriesResultContract> Ask(
            IGetAllArticlesCategoriesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllArticlesCategoriesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllArticlesCategoriesLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}