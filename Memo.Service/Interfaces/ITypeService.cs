using Memo.Domain.ViewModels;

namespace Memo.Service.Interfaces
{
    public interface ITypeService
    {
        List<TypeViewModel> GetAll();
    }
}

