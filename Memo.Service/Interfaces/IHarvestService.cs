using Memo.Domain.ViewModels;

namespace Memo.Service.Interfaces
{
    public interface IHarvestService
    {
        List<HarvestViewModel> GetAll();
    }
}

