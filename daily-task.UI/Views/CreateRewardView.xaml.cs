using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace daily_task.UI.Views
{
    /// <summary>
    /// Interaction logic for CreateRewardView.xaml
    /// </summary>
    public partial class CreateRewardView : UserControl
    {
        public CreateRewardView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}