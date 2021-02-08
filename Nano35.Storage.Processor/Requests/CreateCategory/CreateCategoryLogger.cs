﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.CreateCategory
{
    public class CreateCategoryLogger :
        IPipelineNode<ICreateCategoryRequestContract, ICreateCategoryResultContract>
    {
        private readonly ILogger<CreateCategoryLogger> _logger;
        private readonly IPipelineNode<ICreateCategoryRequestContract, ICreateCategoryResultContract> _nextNode;

        public CreateCategoryLogger(
            ILogger<CreateCategoryLogger> logger,
            IPipelineNode<ICreateCategoryRequestContract, ICreateCategoryResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateCategoryResultContract> Ask(
            ICreateCategoryRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateCategoryLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateCategoryLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}