using System.Windows;
using System.Windows.Controls;
using Memo.Domain.ViewModels;
using Memo.Service.Interfaces;

namespace Memo
{
    public partial class UpdatePage : Page
    {
        private readonly List<PlantingViewModel> _plantingViewModels;
        private readonly List<TypeViewModel> _typeViewModels;
        private readonly List<HarvestViewModel> _harvestViewModels;

        private readonly IVegetableService _vegetableService;

        public UpdatePage(IPlantingService plantingService, IHarvestService harvestService, ITypeService typeService, IVegetableService vegetableService)
        {
            InitializeComponent();

            _vegetableService = vegetableService;
            _plantingViewModels = plantingService.GetAll();
            _typeViewModels = typeService.GetAll();
            _harvestViewModels = harvestService.GetAll();
            comboBoxType.ItemsSource = _typeViewModels.Select(x => x.TypeV).Distinct();
            comboBoxPlanting.ItemsSource = _plantingViewModels.Select(x => x.Planting).Distinct();
            comboBoxHarvest.ItemsSource = _harvestViewModels.Select(x => x.HarvestTime).Distinct();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VegetableViewModel vegetableViewModel = new();
                if (Name.Text.Trim() != string.Empty)
                {
                    if (comboBoxType.SelectedItem != null &&
                        comboBoxHarvest.SelectedItem != null &&
                        comboBoxPlanting.SelectedItem != null)
                    {
                        if (double.TryParse(vegetableHeight.Text, out double height))
                        {
                            _vegetableService.Create(new VegetableViewModel
                            {
                                Name = Name.Text,
                                HeightSm = height,
                                TypeName = comboBoxType.SelectedItem.ToString()!,
                                PlantingTime = Convert.ToDateTime(comboBoxPlanting.SelectedItem!),
                                HarvestTime = Convert.ToInt32(comboBoxHarvest.SelectedItem!),
                            });
                        }
                    }
                }
                if (searchName.Text.Trim() != string.Empty)
                {
                    _vegetableService.Delete(searchName.Text);
                    _vegetableService.Edit(searchName.Text.Trim(), vegetableViewModel);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            Content = null;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }
    }
}

