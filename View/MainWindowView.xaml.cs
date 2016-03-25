using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            TreeViewTasks.ItemsSource = ((MainWindowViewModel) DataContext).UserTasks;
        }

        private void ButtonAddGroup_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
