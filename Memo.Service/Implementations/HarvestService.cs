using Memo.Domain.Models;
using Memo.Domain.ViewModels;
using Memo.Service.Interfaces;
using Memo.DAL.Interfaces;

namespace Memo.Service.Implementations
{
    public class HarvestService(IBaseRepository<Harvest> harvestRepository) : IHarvestService
    {
        private readonly IBaseRepository<Harvest> _harvestRepository = harvestRepository;

        public List<HarvestViewModel> GetAll()
        {
            try
            {
                List<Harvest> harvest = _harvestRepository.ReadAll();
                List<HarvestViewModel> harvestViewModels = [];
                for (int i = 0; i < harvest.Count; i++)
                {
                    ArgumentNullException.ThrowIfNull(harvest[i]);
                    harvestViewModels.Add(harvest[i]);
                }

                return harvestViewModels;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"[GetAll]:Объект Harvest не найден");
            }
            catch (Exception ex)
            {
                throw new Exception($"[GetAll]:{ex.Message}");
            }
        }
    }
}

