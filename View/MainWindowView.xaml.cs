using System.Windows;
using Model;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowViewModel ViewModel { get; set; }
        public MainWindowView()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            DataContext = ViewModel;
            TreeViewTasks.ItemsSource = ViewModel.SuperCollectionTasks;
        }

        private void ButtonAddGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TreeViewTasks_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var task = e.NewValue as UserTask;
            if (task != null)
            {
               ViewModel.SelectedTask = task;
               
            }
        }
    }
}
