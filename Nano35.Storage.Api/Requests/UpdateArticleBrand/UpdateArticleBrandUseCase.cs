using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleBrand
{
    public class UpdateArticleBrandUseCase :
        EndPointNodeBase<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateArticleBrandUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateArticleBrandResultContract> Ask(
            IUpdateArticleBrandRequestContract input) => 
            (await (new UpdateArticleBrandRequest(_bus)).GetResponse(input));
    }
}