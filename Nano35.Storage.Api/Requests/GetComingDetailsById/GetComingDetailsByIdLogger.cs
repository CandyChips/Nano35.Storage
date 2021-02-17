﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetComingDetailsById
{
    public class GetComingDetailsByIdLogger :
        IPipelineNode<
            IGetComingDetailsByIdRequestContract, 
            IGetComingDetailsByIdResultContract>
    {
        private readonly ILogger<GetComingDetailsByIdLogger> _logger;
        private readonly IPipelineNode<
            IGetComingDetailsByIdRequestContract,
            IGetComingDetailsByIdResultContract> _nextNode;

        public GetComingDetailsByIdLogger(
            ILogger<GetComingDetailsByIdLogger> logger,
            IPipelineNode<
                IGetComingDetailsByIdRequestContract,
                IGetComingDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetComingDetailsByIdResultContract> Ask(
            IGetComingDetailsByIdRequestContract input)
        {
            _logger.LogInformation($"GetComingDetailsByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetComingDetailsByIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}