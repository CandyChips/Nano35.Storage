﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllMoves
{
    public class LoggedGetAllMovesRequest :
        IPipelineNode<
            IGetAllMovesRequestContract, 
            IGetAllMovesResultContract>
    {
        private readonly ILogger<LoggedGetAllMovesRequest> _logger;
        private readonly IPipelineNode<
            IGetAllMovesRequestContract,
            IGetAllMovesResultContract> _nextNode;

        public LoggedGetAllMovesRequest(
            ILogger<LoggedGetAllMovesRequest> logger,
            IPipelineNode<
                IGetAllMovesRequestContract,
                IGetAllMovesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllMovesResultContract> Ask(
            IGetAllMovesRequestContract input)
        {
            _logger.LogInformation($"GetAllMovesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllMovesLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}