using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOnStorage
{
    public class GetAllPlacesOnStorageValidatorErrorResult : 
        IGetAllPlacesOnStorageErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllPlacesOnStorageRequest:
        PipeNodeBase<IGetAllPlacesOnStorageContract, IGetAllPlacesOnStorageResultContract>
    {
        public ValidatedGetAllPlacesOnStorageRequest(
            IPipeNode<IGetAllPlacesOnStorageContract, IGetAllPlacesOnStorageResultContract> next) :
            base(next) { }

        public override async Task<IGetAllPlacesOnStorageResultContract> Ask(
            IGetAllPlacesOnStorageContract input)
        {
            if (false)
            {
                return new GetAllPlacesOnStorageValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}