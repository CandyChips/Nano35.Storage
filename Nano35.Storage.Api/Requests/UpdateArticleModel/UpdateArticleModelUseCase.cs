using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleModel
{
    public class UpdateArticleModelUseCase :
        EndPointNodeBase<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateArticleModelUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateArticleModelResultContract> Ask
            (IUpdateArticleModelRequestContract input) => 
            (await (new UpdateArticleModelRequest(_bus)).GetResponse(input));
    }
}