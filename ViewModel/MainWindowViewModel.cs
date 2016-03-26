using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;
using ViewModel.Annotations;
using TaskStatus = Model.TaskStatus;

namespace ViewModel
{

    public class UserTasksCollection : ObservableCollection<UserTask>
    {
        public string GroupName { get; set; }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private UserTask _selectedTask;
        private UserTasksCollection _selectedUserTasksCollection;
        private bool _isUserTaskSelected;
        private bool _isUserTasksCollectionSelected;
        private bool _isInProgressUserTaskCreate;
        private bool _isInProgressUserTasksCollectionCreate;
        private WorkingMode _workingMode;
        //private bool _isWorkingWithSelectedTask;
        // Истинная коллекция всех задач
        //public ObservableCollection <UserTask> UserTasks { get; set; }
        public UserTasksCollection UserTasks { get; set; }

        public UserTasksCollection ExpiredTasks { get; set; }

        public UserTasksCollection ExpireTomorrowTasks { get; set; }

        public UserTasksCollection DefaultTasks { get; set; }

        // Иерархическая коллекция всех групп
        public ObservableCollection<UserTasksCollection> SuperCollectionTasks { get; set; }

        public MainWindowViewModel()
        {
            AlwaysFalse = false;

            #region UserTasks Initialization
            //UserTasks = new ObservableCollection<UserTask>
            //{
            //    new UserTask
            //    {
            //        Name = "New Task 1",
            //        Description = "Description 1",
            //        DueDate = Convert.ToDateTime("2016-04-1"),
            //        Status = TaskStatus.New,
            //        Group = "Default"
            //    },
            //    new UserTask
            //    {
            //        Name = "Postponded Task 2",
            //        Description = "Description 2",
            //        DueDate = Convert.ToDateTime("2015-01-01"),
            //        Status = TaskStatus.Postponded,
            //        Group = "Default"
            //    },
            //    new UserTask
            //    {
            //        Name = "In Progress Task 4",
            //        Description = "Description 1",
            //        DueDate = Convert.ToDateTime("2016-05-03"),
            //        Status = TaskStatus.InProgress,
            //        Group = "Default"
            //    },
            //    new UserTask
            //    {
            //        Name = "Completed Task 5",
            //        Description = "Description 1",
            //        DueDate = Convert.ToDateTime("2016-03-1"),
            //        Status = TaskStatus.Completed,
            //        Group = "Default"
            //    },
            //    new UserTask
            //    {
            //        Name = "Canceled Task 6",
            //        Description = "Description 1",
            //        DueDate = Convert.ToDateTime("2017-03-30"),
            //        Status = TaskStatus.Canceled,
            //        Group = "Default"
            //    }
            //};
            #endregion


#region UserTasks Initialization 2
            UserTasks = new UserTasksCollection
            {
                new UserTask
                {
                    Name = "New Task 1",
                    Description = "Description 1",
                    DueDate = Convert.ToDateTime("2016-04-1"),
                    Status = TaskStatus.New,
                    Group = "Default"
                },
                new UserTask
                {
                    Name = "Postponded Task 2",
                    Description = "Description 2",
                    DueDate = Convert.ToDateTime("2015-01-01"),
                    Status = TaskStatus.Postponded,
                    Group = "Default"
                },
                new UserTask
                {
                    Name = "In Progress Task 4",
                    Description = "Description 1",
                    DueDate = Convert.ToDateTime("2016-05-03"),
                    Status = TaskStatus.InProgress,
                    Group = "Default"
                },
                new UserTask
                {
                    Name = "Completed Task 5",
                    Description = "Description 1",
                    DueDate = Convert.ToDateTime("2016-03-1"),
                    Status = TaskStatus.Completed,
                    Group = "Default"
                },
                new UserTask
                {
                    Name = "Canceled Task 6",
                    Description = "Description 1",
                    DueDate = Convert.ToDateTime("2017-03-30"),
                    Status = TaskStatus.Canceled,
                    Group = "Default"
                }
            };
            UserTasks.GroupName = "All";

            #endregion

            ExpiredTasks = new UserTasksCollection()
            {
                GroupName = "Expired"
            };

            ExpireTomorrowTasks = new UserTasksCollection()
            {
                GroupName = "Expire Tomorrow"
            };

            DefaultTasks = new UserTasksCollection()
            {
                GroupName = "Default"
               
            };

            // Добавляем элемнты из UserTasks в DefaultTasks
            // TODO: (Должно быть лучшее решение для копирования)
            foreach (var u in UserTasks)
            {
                DefaultTasks.Add(u);
            }

            SuperCollectionTasks = new ObservableCollection<UserTasksCollection>
            {
                DefaultTasks,
                ExpiredTasks,
                ExpireTomorrowTasks
            };
            SelectedTask = null;
            WorkingMode = WorkingMode.WorkingModeDefault;
            SelectedUserTasksCollection = null;
            IsUserTaskSelected = false;
            IsUserTasksCollectionSelected = false;
            //IsWorkingWithSelectedTask = false;
        }

        public WorkingMode WorkingMode
        {
            get { return _workingMode; }
            set
            {
                _workingMode = value; 
                OnPropertyChanged(nameof(WorkingMode));
            }
        }

        public bool IsInDefaultMode => 
            WorkingMode == WorkingMode.WorkingModeDefault;

        public bool IsInUserTaskEditMode => 
            WorkingMode == WorkingMode.WorkingModeUserTaskEdit;

        public bool IsInUserTaskCreateMode => 
            WorkingMode == WorkingMode.WorkingModeUserTaskCreate;

        public bool IsInUserTasksCollectionEditMode => 
            WorkingMode == WorkingMode.WorkingModeUserTasksCollectionEdit;

        public bool IsInUserTasksCollectionCreateMode => 
            WorkingMode == WorkingMode.WorkingModeUserTasksCollectionCreate;

        public bool IsInCreating =>
            WorkingMode == WorkingMode.WorkingModeDefault ||
            WorkingMode == WorkingMode.WorkingModeUserTaskEdit ||
            WorkingMode == WorkingMode.WorkingModeUserTasksCollectionEdit;

        public bool IsAddingNewGroupAvailable =>
            WorkingMode == WorkingMode.WorkingModeDefault ||
            WorkingMode == WorkingMode.WorkingModeUserTaskEdit ||
            WorkingMode == WorkingMode.WorkingModeUserTasksCollectionEdit;

        public bool IsAddingNewUserTaskAvailable =>
            WorkingMode == WorkingMode.WorkingModeUserTasksCollectionEdit;

        public bool IsWorkingWithSelectedGroup =>
            WorkingMode == WorkingMode.WorkingModeUserTasksCollectionEdit ||
            WorkingMode == WorkingMode.WorkingModeUserTasksCollectionCreate;

        public bool IsWorkingWithSelectedTask
        {
            get
            {
                //return _isWorkingWithSelectedTask;
               return WorkingMode == WorkingMode.WorkingModeUserTaskEdit ||
                   WorkingMode == WorkingMode.WorkingModeUserTaskCreate;
            }
            //set
            //{
            //    _isWorkingWithSelectedTask = value;
            //    OnPropertyChanged(nameof(IsWorkingWithSelectedTask));
            //}
        }

        public bool AlwaysFalse { get; }

        public UserTask SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                IsUserTaskSelected = (value != null);
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        public bool IsUserTaskSelected
        {
            get { return _isUserTaskSelected; }
            set
            {
                _isUserTaskSelected = value;
            //    IsUserTasksCollectionSelected = !value;
                if (value)
                {
                    IsUserTasksCollectionSelected = false;
                }
                IsInProgressUserTaskCreating = (value) || IsInProgressUserTaskCreating ;
                OnPropertyChanged(nameof(IsUserTaskSelected));
            }
        }

        public bool IsInProgressUserTaskCreating
        {
            get { return _isInProgressUserTaskCreate; }
            set
            {
                _isInProgressUserTaskCreate = value; 
                OnPropertyChanged(nameof(IsInProgressUserTaskCreating));
            }
        }

        public UserTasksCollection SelectedUserTasksCollection
        {
            get { return _selectedUserTasksCollection; }
            set
            {
                _selectedUserTasksCollection = value;
                
                IsUserTasksCollectionSelected = (value != null);
                OnPropertyChanged(nameof(SelectedUserTasksCollection));
            }
        }

        public bool IsUserTasksCollectionSelected
        {
            get { return _isUserTasksCollectionSelected; }
            set
            {
                _isUserTasksCollectionSelected = value;
                //  IsUserTaskSelected = !value;
                if (value)
                {
                    IsUserTaskSelected = false;
                }
                OnPropertyChanged(nameof(IsUserTasksCollectionSelected));
            }
        }

        public bool IsInProgressUserTasksCollectionCreating
        {
            get { return _isInProgressUserTasksCollectionCreate; }
            set
            {
                _isInProgressUserTasksCollectionCreate = value; 
                OnPropertyChanged(nameof(IsInProgressUserTasksCollectionCreating));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NewUserTasksCollection(string name)
        {
            var newUserTasksCollection = new UserTasksCollection {GroupName = name};
            SuperCollectionTasks.Add(newUserTasksCollection);
            //SelectedUserTasksCollection = newUserTasksCollection;
        }

        public void NewGenericGroupCreate()
        {
            WorkingMode = WorkingMode.WorkingModeUserTasksCollectionCreate;
            var newGroup = new UserTasksCollection {GroupName = "Новая Группа"};
            SelectedUserTasksCollection = newGroup;
        }

        public void SaveNewGroup(UserTasksCollection newGroup)
        {
            SuperCollectionTasks.Add(newGroup);
            WorkingMode = WorkingMode.WorkingModeUserTasksCollectionEdit;
        }

        public void CancelNewGroupCreation()
        {
            SelectedUserTasksCollection = null;
            WorkingMode = WorkingMode.WorkingModeDefault;
        }

        public void NewGenericUserTaskCreate()
        {
            WorkingMode = WorkingMode.WorkingModeUserTaskCreate;
            var newUserTask = new UserTask
            {
                Name = "Новая задача",
                Description = "",
                DueDate = DateTime.Today.AddDays(1),
                Status = Model.TaskStatus.New,
                Group = SelectedUserTasksCollection.GroupName
            };
            SelectedTask = newUserTask;

        }

        public void SaveNewUserTask(UserTask newUserTask)
        {
            SelectedUserTasksCollection.Add(newUserTask);
            WorkingMode = WorkingMode.WorkingModeUserTaskEdit;
        }

        public void CancelNewUserTaskCreation()
        {
            SelectedTask = null;
            WorkingMode = WorkingMode.WorkingModeDefault;
        }

        //public void NewUserTask(string name, string description, DateTime dueDate, Model.TaskStatus status)
        //{
        //    // TODO: Нормально обработать ситуацию, если группа не выбрана
        //    if (SelectedUserTasksCollection == null) return;

        //    var newUserTask = new UserTask
        //    {
        //        Name = name,
        //        Description = description,
        //        DueDate = dueDate,
        //        Status = status,
        //        Group = SelectedUserTasksCollection.GroupName
        //    };
        //    SelectedUserTasksCollection.Add(newUserTask);
        //}

    }
}
