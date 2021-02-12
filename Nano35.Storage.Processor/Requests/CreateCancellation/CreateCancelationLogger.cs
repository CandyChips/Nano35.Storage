﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.CreateCancellation
{
    public class CreateCancellationLogger :
        IPipelineNode<
            ICreateCancellationRequestContract,
            ICreateCancellationResultContract>
    {
        private readonly ILogger<CreateCancellationLogger> _logger;
        private readonly IPipelineNode<
            ICreateCancellationRequestContract, 
            ICreateCancellationResultContract> _nextNode;

        public CreateCancellationLogger(
            ILogger<CreateCancellationLogger> logger,
            IPipelineNode<
                ICreateCancellationRequestContract,
                ICreateCancellationResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Create cancellation logger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"Create cancellation logger ends on: {DateTime.Now}");
            return result;
        }
    }
}