using Memo.DAL.Interfaces;
using Memo.Domain.ViewModels;
using Memo.Domain;
using Memo.Service.Interfaces;

namespace Memo.Service.Implementations
{
    public class PlantingService(IBaseRepository<Planting> plantingRepository) : IPlantingService
    {
        private readonly IBaseRepository<Planting> _plantingRepository = plantingRepository;

        public List<PlantingViewModel> GetAll()
        {
            try
            {
                List<Planting> planting = _plantingRepository.ReadAll();
                List<PlantingViewModel> plantingViewModels = [];
                for (int i = 0; i < planting.Count; i++)
                {
                    ArgumentNullException.ThrowIfNull(planting[i]);
                    plantingViewModels.Add(planting[i]);
                }

                return plantingViewModels;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"[GetAll]:Объект Planting не найден");
            }
            catch (Exception ex)
            {
                throw new Exception($"[GetAll]:{ex.Message}");
            }
        }

    }
}

