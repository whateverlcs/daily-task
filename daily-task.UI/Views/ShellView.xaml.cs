using MahApps.Metro.Controls;
using System.Windows;

namespace daily_task.UI.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        public ShellView()
        {
            InitializeComponent();

            // Este evento dispara toda vez que a janela muda de tamanho (ao trocar de View)
            this.SizeChanged += (s, e) =>
            {
                // Só centraliza se a mudança de tamanho foi causada pelo conteúdo (e não pelo usuário arrastando)
                if (e.HeightChanged || e.WidthChanged)
                {
                    double screenWidth = SystemParameters.WorkArea.Width;
                    double screenHeight = SystemParameters.WorkArea.Height;

                    this.Left = (screenWidth - this.ActualWidth) / 2;
                    this.Top = (screenHeight - this.ActualHeight) / 2;
                }
            };
        }
    }
}