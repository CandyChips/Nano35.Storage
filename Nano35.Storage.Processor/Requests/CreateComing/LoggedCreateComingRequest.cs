﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.CreateComing
{
    public class LoggedCreateComingRequest :
        IPipelineNode<ICreateComingRequestContract, ICreateComingResultContract>
    {
        private readonly ILogger<LoggedCreateComingRequest> _logger;
        private readonly IPipelineNode<ICreateComingRequestContract, ICreateComingResultContract> _nextNode;

        public LoggedCreateComingRequest(
            ILogger<LoggedCreateComingRequest> logger,
            IPipelineNode<ICreateComingRequestContract, ICreateComingResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateComingResultContract> Ask(
            ICreateComingRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateComingLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateComingLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}