﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticlesModels
{
    public class GetAllArticlesModelsLogger :
        IPipelineNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>
    {
        private readonly ILogger<GetAllArticlesModelsLogger> _logger;
        private readonly IPipelineNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract> _nextNode;

        public GetAllArticlesModelsLogger(
            ILogger<GetAllArticlesModelsLogger> logger,
            IPipelineNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllArticlesModelsResultContract> Ask(
            IGetAllArticlesModelsRequestContract input)
        {
            _logger.LogInformation($"GetAllArticlesModelsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllArticlesModelsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}