using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Memo.DAL.ADO.Net;
using Memo.DAL.Interfaces;
using Memo.DAL.Repositories;
using Memo.Domain.Models;
using Memo.Domain.ViewModels;
using Memo.Domain;
using Memo.Service.Implementations;
using Memo.Service.Interfaces;
using System.Windows.Input;

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
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            IDbContext dbContext = new SqlContextLinq();

            IBaseRepository<Vegetable> vegetableRepository = new VegetableRepository(dbContext);
            _vegetableService = new VegetableService(vegetableRepository);

            IBaseRepository<Planting> plantingRepository = new PlantingRepository(dbContext);
            _plantingService = new PlantingService(plantingRepository);

            IBaseRepository<Memo.Domain.Type> typeRepository = new TypeRepository(dbContext);
            _typeService = new TypeService(typeRepository);

            IBaseRepository<Harvest> harvestRepository = new HarvestRepository(dbContext);
            _harvestService = new HarvestService(harvestRepository);

            dataGrid.ItemsSource = _vegetableService.GetAll();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyDescriptor is PropertyDescriptor descriptor)
            {
                e.Column.Header = descriptor.DisplayName;
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case (Key.F1):
                    {
                        PageFrame.Content = new AddPage(_plantingService, _harvestService, _typeService, _vegetableService);
                        dataGrid.ItemsSource = _vegetableService.GetAll();
                    }
                    break;
                case (Key.F2):
                    {
                        if (dataGrid.SelectedItem != null)
                        {
                            dataGrid.SelectedItem = new Vegetable();
                            //dataGrid.ItemsSource = _vegetableService.GetAll();
                        }
                    }
                    break;
                case (Key.F3):
                    {
                        PageFrame.Content = new UpdatePage(_plantingService, _harvestService, _typeService, _vegetableService);
                        dataGrid.ItemsSource = _vegetableService.GetAll();
                    }
                    break;
                case (Key.F4): 
                    {
                        SearchPage searchPage = new(_vegetableService)
                        {
                            DataContext = this
                        };
                        PageFrame.Content = searchPage;
                    }
                    break;
                    case (Key.F5):
                    {
                        GroupPage groupPage = new(_vegetableService, _typeService, _harvestService)
                        {
                            DataContext = this
                        };
                        PageFrame.Content = groupPage;
                    }
                    break;
                    case (Key.F6):
                    {
                        MessageBox.Show($"Общее количесво записей: {_vegetableService.Count()};\n" +
                        $"Средняя высота по всем овощам: {_vegetableService.HeightAvg()};\n" +
                        $"Минимальная высота среди всех овощей: {_vegetableService.HeightMin()};\n" +
                        $"Максимальная высота среди всех овощей: {_vegetableService.HeightMax()};\n");
                    }
                    break;
            }
        }
    }
}