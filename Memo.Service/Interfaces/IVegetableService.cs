using Memo.Domain.ViewModels;

namespace Memo.Service.Interfaces
{
    public interface IVegetableService
    {
        List<VegetableViewModel> GetAll();

        VegetableViewModel GetByName(string name);

        VegetableViewModel Get(int id);

        bool Create(VegetableViewModel viewModel);

        bool Delete(int id);

        bool Delete(string name);

        bool Edit(int id, VegetableViewModel viewModel);

        bool Edit(string name, VegetableViewModel viewModel);

        public List<VegetableViewModel> FindByName(string name);
        public List<VegetableViewModel> GroupByType(string name);

        public List<VegetableViewModel> GroupByHarvest(string name);
        public int Count();
        double HeightAvg();
        double HeightMin();
        double HeightMax();
    }
}

