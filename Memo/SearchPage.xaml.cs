using Memo;
using Memo.Service.Interfaces;
using System.Windows.Controls;
using System.Windows;

namespace Memo;

public partial class SearchPage : Page
{
    private readonly IVegetableService _vegetableService;
    public SearchPage(IVegetableService vegetableService)
    {
        InitializeComponent();
        _vegetableService = vegetableService;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Content = null;
    }

    private void SubmitButton_Click(object sender, RoutedEventArgs e)
    {
        string name = vegetableName.Text;
            if (DataContext is MainWindow mainWindow)
            {
                mainWindow.dataGrid.ItemsSource = _vegetableService.FindByName(name);
            }
            Content = null;
    }
}
