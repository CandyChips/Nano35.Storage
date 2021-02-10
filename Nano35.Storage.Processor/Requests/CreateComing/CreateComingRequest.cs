using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateComing
{
    public class CreateMoveRequest :
        IPipelineNode<ICreateMoveRequestContract, ICreateMoveResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateMoveRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateMoveSuccessResultContract : 
            ICreateMoveSuccessResultContract
        {
            
        }
        
        public async Task<ICreateMoveResultContract> Ask(
            ICreateMoveRequestContract input,
            CancellationToken cancellationToken)
        {
            var coming = new Coming()
            {
                Id = input.NewId,
                Date = DateTime.Now,
                Number = input.Number,
                InstanceId = input.IntsanceId,
            };
            
            var comingDetails = input.Details
                .Select(a => new ComingDetail()
                {
                    ComingId = input.NewId,
                    Count = a.Count, 
                    Price = a.Price,
                    StorageItemId = a.StringItemId, 
                    ToUnitId = input.ToUnitId
                });
                    
            return new CreateMoveSuccessResultContract();
        }
    }
}