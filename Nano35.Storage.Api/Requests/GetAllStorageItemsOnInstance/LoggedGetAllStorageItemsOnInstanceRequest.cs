﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemsOnInstance
{
    public class LoggedGetAllStorageItemsOnInstanceRequest :
        PipeNodeBase<
            IGetAllStorageItemsOnInstanceContract, 
            IGetAllStorageItemsOnInstanceResultContract>
    {
        private readonly ILogger<LoggedGetAllStorageItemsOnInstanceRequest> _logger;

        public LoggedGetAllStorageItemsOnInstanceRequest(
            ILogger<LoggedGetAllStorageItemsOnInstanceRequest> logger,
            IPipeNode<IGetAllStorageItemsOnInstanceContract,
                IGetAllStorageItemsOnInstanceResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllStorageItemsOnInstanceResultContract> Ask(
            IGetAllStorageItemsOnInstanceContract input)
        {
            _logger.LogInformation($"GetAllStorageItemsOnInstanceLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllStorageItemsOnInstanceLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}