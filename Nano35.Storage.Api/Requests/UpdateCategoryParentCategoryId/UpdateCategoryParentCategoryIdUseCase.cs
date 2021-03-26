using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateCategoryParentCategoryId
{
    public class UpdateCategoryParentCategoryIdUseCase :
        EndPointNodeBase<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateCategoryParentCategoryIdUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateCategoryParentCategoryIdResultContract> Ask
            (IUpdateCategoryParentCategoryIdRequestContract input) => 
            (await (new UpdateCategoryParentCategoryIdRequest(_bus)).GetResponse(input));
    }
}