using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateCategoryName
{
    public class UpdateCategoryNameUseCase :
        EndPointNodeBase<IUpdateCategoryNameRequestContract, IUpdateCategoryNameResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateCategoryNameUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateCategoryNameResultContract> Ask
            (IUpdateCategoryNameRequestContract input) => 
            (await (new UpdateCategoryNameRequest(_bus)).GetResponse(input));
    }
}