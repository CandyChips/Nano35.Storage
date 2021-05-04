using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateCategory
{
    public class CreateCategoryRequest :
        UseCaseEndPointNodeBase<ICreateCategoryRequestContract, ICreateCategoryResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateCategoryRequest(ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<ICreateCategoryResultContract>> Ask(
            ICreateCategoryRequestContract input,
            CancellationToken cancellationToken)
        {
            var category =
                new Category()
                    {Id = input.NewId,
                     InstanceId = input.InstanceId,
                     ParentCategoryId = input.ParentCategoryId == Guid.Empty ? 
                         null : 
                         input.ParentCategoryId,
                     Name = input.Name,
                     IsDeleted = false};
            await _context.AddAsync(category, cancellationToken);

            return new UseCaseResponse<ICreateCategoryResultContract>(new CreateCategorySuccessResultContract());
        }
    }
}