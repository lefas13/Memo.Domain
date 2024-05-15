using Memo;
using Memo.Service.Interfaces;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

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
        this.KeyDown += new KeyEventHandler(GroupPage_KeyDown);
    }

    private void GroupPage_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case (Key.Enter):
                {
                    if (By.Text == "Виду овоща")
                    {
                        Group.ItemsSource = _typeService.GetAll().Select(x => x.TypeV);
                    }
                    else
                    {
                        Group.ItemsSource = _harvestService.GetAll().Select(x => x.HarvestTime);
                    }
                }
                break;
        }
    }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
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

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Content = null;
    }
}