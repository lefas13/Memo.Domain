using Memo;
using Memo.Service.Interfaces;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Memo.Domain.ViewModels;

namespace Memo;

public partial class GroupPage : Page
{
    private readonly IVegetableService _vegetableService;
    private readonly ITypeService _typeService;
    private readonly IHarvestService _harvestService;
    public GroupPage(IVegetableService vegetableService, ITypeService typeService, IHarvestService harvestService)
    {
        InitializeComponent();
        _vegetableService = vegetableService;
        _typeService = typeService;
        _harvestService = harvestService;
    }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (Group.SelectedItem != null)
            {
                string name = Group.SelectedItem.ToString()!;
                if (DataContext is MainWindow mainWindow)
                {
                    if (By.Text == "Виду овоща")
                    {
                        mainWindow.dataGrid.ItemsSource = _vegetableService.GroupByType(name);
                    }
                    else
                    {
                        mainWindow.dataGrid.ItemsSource = _vegetableService.GroupByHarvest(name);
                    }
                }
                Content = null;
            }
        }
        catch (Exception ex)
        {
            if (DataContext is MainWindow mainWindow)
                mainWindow.dataGrid.ItemsSource = new List<VegetableViewModel>();
            Content = null;
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Content = null;
    }

    private void By_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBox comboBox = (ComboBox)sender;
        ComboBoxItem selectedComboBoxItem = (ComboBoxItem)comboBox.SelectedItem;
        if (selectedComboBoxItem.Content.ToString() == "Виду овоща")
        {
            Group.ItemsSource = _typeService.GetAll().Select(x => x.TypeV);
        }
        else if(selectedComboBoxItem.Content.ToString() == "Времени сбора урожая")
        {
            Group.ItemsSource = _harvestService.GetAll().Select(x => x.HarvestTime);
        }
    }
}