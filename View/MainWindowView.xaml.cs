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

        private void TreeViewTasks_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var value = e.NewValue;
            if (value is UserTask)
            {
                ViewModel.SelectedTask = (UserTask)value;
            }
            else if (value is TaskGroup)
            {
                ViewModel.SelectedTaskGroup = (TaskGroup)value;
            }

        }
    }
}
