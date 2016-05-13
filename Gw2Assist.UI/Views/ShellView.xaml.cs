using System.Windows;

namespace Gw2Assist.UI.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        private bool WindowIsBeingDragged = false;

        public ShellView()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.WindowIsBeingDragged)
            {
                this.WindowIsBeingDragged = false;
            }
            else
            {
                var menu = this.mainGrid.ContextMenu;
                menu.PlacementTarget = this.mainGrid;
                menu.IsOpen = true;
            }
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.WindowIsBeingDragged = true;
                this.DragMove();
            }
        }
    }
}
