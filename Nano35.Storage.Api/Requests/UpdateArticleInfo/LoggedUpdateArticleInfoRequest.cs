﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleInfo
{
    public class LoggedUpdateArticleInfoRequest :
        PipeNodeBase<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>
    {
        private readonly ILogger<LoggedUpdateArticleInfoRequest> _logger;

        public LoggedUpdateArticleInfoRequest(
            ILogger<LoggedUpdateArticleInfoRequest> logger,
            IPipeNode<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateArticleInfoResultContract> Ask(
            IUpdateArticleInfoRequestContract input)
        {
            _logger.LogInformation($"UpdateArticleInfoLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateArticleInfoLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}