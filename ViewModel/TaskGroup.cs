using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;
using ViewModel.Annotations;

namespace ViewModel
{
    public class TaskGroup : INotifyPropertyChanged
    {
        private string _groupName;
        private ObservableCollection<UserTask> _userTasks;

        public ObservableCollection<UserTask> UserTasks
        {
            get
            {   
                return _userTasks
                  ?? (_userTasks = new ObservableCollection<UserTask>());
            }
            set
            {
                _userTasks = value; 
                OnPropertyChanged(nameof(UserTasks));
            }
        }

        private void GroupNameUpdate()
        {
            if (UserTasks != null)
            {
                foreach (var task in UserTasks)
                {
                    task.Group = _groupName;
                }
            }
        }

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value; 
                GroupNameUpdate();
                OnPropertyChanged(nameof(GroupName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}