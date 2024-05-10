using Memo.Domain.ViewModels;

namespace Memo.Service.Interfaces
{
    public interface IPlantingService
    {
        List<PlantingViewModel> GetAll();
    }
}

