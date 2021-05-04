using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemConditions
{
    public class GetAllStorageItemConditionsRequest :
        UseCaseEndPointNodeBase<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllStorageItemConditionsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<IGetAllStorageItemConditionsResultContract>> Ask
            (IGetAllStorageItemConditionsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .StorageItemConditions
                .Select(a =>
                    new StorageItemConditionViewModel()
                        {Id = a.Id, 
                         Name = a.Name})
                .ToListAsync(cancellationToken: cancellationToken);

            return new UseCaseResponse<IGetAllStorageItemConditionsResultContract>(
                new GetAllStorageItemConditionsResultContract() {Data = result});
        }
    }   
}