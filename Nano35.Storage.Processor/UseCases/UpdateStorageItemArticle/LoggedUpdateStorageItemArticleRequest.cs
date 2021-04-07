﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemArticle
{
    public class LoggedUpdateStorageItemArticleRequest :
        PipeNodeBase<
            IUpdateStorageItemArticleRequestContract, 
            IUpdateStorageItemArticleResultContract>
    {
        private readonly ILogger<LoggedUpdateStorageItemArticleRequest> _logger;

        public LoggedUpdateStorageItemArticleRequest(
            ILogger<LoggedUpdateStorageItemArticleRequest> logger,
            IPipeNode<IUpdateStorageItemArticleRequestContract,
                IUpdateStorageItemArticleResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateStorageItemArticleResultContract> Ask(
            IUpdateStorageItemArticleRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateStorageItemArticleLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"UpdateStorageItemArticleLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}