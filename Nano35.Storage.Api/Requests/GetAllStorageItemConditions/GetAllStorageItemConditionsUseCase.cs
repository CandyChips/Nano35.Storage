﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemConditions
{
    public class GetAllStorageItemConditionsUseCase :
        EndPointNodeBase<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>
    {
        private readonly IBus _bus;
        public GetAllStorageItemConditionsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllStorageItemConditionsResultContract> Ask(
            IGetAllStorageItemConditionsRequestContract input) => 
            (await (new GetAllStorageItemConditionsRequest(_bus)).GetResponse(input));
    }
}