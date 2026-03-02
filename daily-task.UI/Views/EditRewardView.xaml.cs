using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace daily_task.UI.Views
{
    /// <summary>
    /// Interaction logic for EditRewardView.xaml
    /// </summary>
    public partial class EditRewardView : UserControl
    {
        public EditRewardView()
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