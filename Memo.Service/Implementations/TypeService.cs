using Memo.DAL.Interfaces;
using Memo.Domain.ViewModels;
using Memo.Service.Interfaces;

namespace Memo.Service.Implementations
{
    public class TypeService(IBaseRepository<Domain.Type> typeRepository) : ITypeService
    {
        private readonly IBaseRepository<Domain.Type> _typeRepository = typeRepository;

        public List<TypeViewModel> GetAll()
        {
            try
            {
                List<Domain.Type> type = _typeRepository.ReadAll();
                List<TypeViewModel> typeViewModels = [];
                for (int i = 0; i < type.Count; i++)
                {
                    ArgumentNullException.ThrowIfNull(type[i]);
                    typeViewModels.Add(type[i]);
                }

                return typeViewModels;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"[GetAll]:Объект Type не найден");
            }
            catch (Exception ex)
            {
                throw new Exception($"[GetAll]:{ex.Message}");
            }
        }
    }
}

