﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleInfo
{
    public class LoggedUpdateArticleInfoRequest :
        IPipelineNode<
            IUpdateArticleInfoRequestContract,
            IUpdateArticleInfoResultContract>
    {
        private readonly ILogger<LoggedUpdateArticleInfoRequest> _logger;
        private readonly IPipelineNode<
            IUpdateArticleInfoRequestContract, 
            IUpdateArticleInfoResultContract> _nextNode;

        public LoggedUpdateArticleInfoRequest(
            ILogger<LoggedUpdateArticleInfoRequest> logger,
            IPipelineNode<
                IUpdateArticleInfoRequestContract, 
                IUpdateArticleInfoResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateArticleInfoResultContract> Ask(
            IUpdateArticleInfoRequestContract input)
        {
            _logger.LogInformation($"UpdateArticleInfoLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateArticleInfoLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}