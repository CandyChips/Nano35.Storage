using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllWarehouseNames
{
    public class GetAllWarehouseNamesValidatorErrorResult : 
        IGetAllWarehouseNamesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllWarehouseNamesRequest:
        PipeNodeBase<
            IGetAllWarehouseNamesRequestContract, 
            IGetAllWarehouseNamesResultContract>
    {

        public ValidatedGetAllWarehouseNamesRequest(
            IPipeNode<IGetAllWarehouseNamesRequestContract,
                IGetAllWarehouseNamesResultContract> next) : base(next)
        {}

        public override async Task<IGetAllWarehouseNamesResultContract> Ask(
            IGetAllWarehouseNamesRequestContract input)
        {
            if (false)
            {
                return new GetAllWarehouseNamesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}