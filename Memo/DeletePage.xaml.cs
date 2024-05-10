using System.Windows;
using System.Windows.Controls;
using Memo.Service.Interfaces;

namespace Memo
{
    public partial class DeletePage : Page
    {
        private readonly IVegetableService _vegetableService;

        public DeletePage(IVegetableService vegetableService)
        {
            InitializeComponent();

            _vegetableService = vegetableService;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Name.Text.Trim() != string.Empty)
                {
                    _vegetableService.Delete(Name.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Content = null;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }
    }
}

