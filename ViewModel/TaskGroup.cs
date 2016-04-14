using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;
using ViewModel.Annotations;

namespace ViewModel
{
    // √руппа задач
    public class TaskGroup : INotifyPropertyChanged
    {
        public TaskGroup()
        {
            IsAutoGroup = false;
        }

        // »м€ группы
        private string _groupName;

        // —писок задач
        private ObservableCollection<UserTask> _userTasks;

        // явл€етс€ ли группа автогруппой
        public bool IsAutoGroup { get; set; }

        // 
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
        
        // ќбновление имени группы дл€ всех
        // задач, вход€щих в группу
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