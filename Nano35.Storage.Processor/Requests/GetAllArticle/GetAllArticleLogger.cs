﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllArticle
{
    public class GetAllArticlesLogger :
        IPipelineNode<
            IGetAllArticlesRequestContract, 
            IGetAllArticlesResultContract>
    {
        private readonly ILogger<GetAllArticlesLogger> _logger;
        private readonly IPipelineNode<
            IGetAllArticlesRequestContract,
            IGetAllArticlesResultContract> _nextNode;

        public GetAllArticlesLogger(
            ILogger<GetAllArticlesLogger> logger,
            IPipelineNode<
                IGetAllArticlesRequestContract,
                IGetAllArticlesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllArticlesResultContract> Ask(
            IGetAllArticlesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllArticlesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllArticlesLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}