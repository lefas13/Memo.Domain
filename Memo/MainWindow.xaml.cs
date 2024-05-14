using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Memo.DAL.ADO.Net;
using Memo.DAL.Interfaces;
using Memo.DAL.Repositories;
using Memo.Domain.Models;
using Memo.Domain;
using Memo.Service.Implementations;
using Memo.Service.Interfaces;

namespace Memo
{
    public partial class MainWindow : Window
    {
        private readonly IVegetableService _vegetableService;
        private readonly IPlantingService _plantingService;
        private readonly ITypeService _typeService;
        private readonly IHarvestService _harvestService;

        public MainWindow()
        {
            InitializeComponent();
            IDbContext dbContext = new SqlContextLinq();

            IBaseRepository<Vegetable> vegetableRepository = new VegetableRepository(dbContext);
            _vegetableService = new VegetableService(vegetableRepository);

            IBaseRepository<Planting> plantingRepository = new PlantingRepository(dbContext);
            _plantingService = new PlantingService(plantingRepository);

            IBaseRepository<Memo.Domain.Type> typeRepository = new TypeRepository(dbContext);
            _typeService = new TypeService(typeRepository);

            IBaseRepository<Harvest> harvestRepository = new HarvestRepository(dbContext);
            _harvestService = new HarvestService(harvestRepository);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = _vegetableService.GetAll();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyDescriptor is PropertyDescriptor descriptor)
            {
                e.Column.Header = descriptor.DisplayName;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new AddPage(_plantingService, _harvestService, _typeService, _vegetableService);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new DeletePage(_vegetableService);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new UpdatePage(_plantingService, _harvestService, _typeService, _vegetableService);
        }
    }
}