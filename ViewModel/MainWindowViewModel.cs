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

    public class UserTasksCollection : ObservableCollection<UserTask>, INotifyPropertyChanged
    {
        private string _groupName;

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value; 
                OnPropertyChanged(nameof(GroupName));
            }
        }

        protected override event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private UserTask _selectedTask;
        private UserTasksCollection _selectedUserTasksCollection;
        private bool _isUserTaskSelected;
        private bool _isUserTasksCollectionSelected;
        private WorkingMode _workingMode;
        public UserTasksCollection UserTasks { get; set; }

        public UserTasksCollection ExpiredTasks { get; set; }

        public UserTasksCollection ExpireTomorrowTasks { get; set; }

        public UserTasksCollection DefaultTasks { get; set; }

        // Иерархическая коллекция всех групп
        public ObservableCollection<UserTasksCollection> SuperCollectionTasks { get; set; }

        public MainWindowViewModel()
        {
         

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

            // Поле для хранения старого значения
            // имени группы (нужно для команды редактирования)
            _oldGroupName = "";

            // Редактирование группу
            // Начать редактировать группу можно из режима обзора
            RelayCommandGroupEdit = 
                new RelayCommand(GroupEdit, param => IsInModeGroupVeiw );

            // Отменить редактирование группы
            // Можно в режиме редактирования
            RelayCommandGroupEditCancel = 
                new RelayCommand(GroupEditCancel, param => IsInModeGroupEdit);

            // Сохранить изменения в группе
            // Можно в режиме редактирования
            RelayCommandGroupEditSave =
                new RelayCommand(GroupEditSave, param => IsInModeGroupEdit);

            // Создать новую группу
            // Создать группу можно, если не включен режим 
            // редактирование или создания новой группы или задачи
            RelayCommandGroupCreate = 
                new RelayCommand(GroupCreate, param => IsNotInEditingOrCreating);            
            
            // Сохранить созданную группу
            // Можно в режиме создания группы
            RelayCommandGroupSaveNew =
                new RelayCommand(GroupCreateSave, param => IsInModeGroupGreate);

            // Отменить создание группы
            // Можно в режиме создания группы
            RelayCommandGroupCreateCancel = 
                new RelayCommand(GroupCreateCancel, param => IsInModeGroupGreate);

        

            //IsWorkingWithSelectedTask = false;
        }

        private string _oldGroupName;

        public WorkingMode WorkingMode
        {
            get { return _workingMode; }
            set
            {
                _workingMode = value; 
                OnPropertyChanged(nameof(WorkingMode));

                // Обновление вычисляемых свойств
                // сами они не обновятся
                OnPropertyChanged(nameof(IsInDefaultMode));
                OnPropertyChanged(nameof(IsInModeTaskView));
                OnPropertyChanged(nameof(IsInModeTaskEdit));
                OnPropertyChanged(nameof(IsInModeTaskCreate));
                OnPropertyChanged(nameof(IsInModeGroupVeiw));
                OnPropertyChanged(nameof(IsInModeGroupEdit));
                OnPropertyChanged(nameof(IsInModeGroupGreate));

                OnPropertyChanged(nameof(IsWorkingWithGroup));
                OnPropertyChanged(nameof(IsWorkingWithTask));
                OnPropertyChanged(nameof(IsInModeGroupViewOrEdit));
                OnPropertyChanged(nameof(IsNotInEditingOrCreating));
                OnPropertyChanged(nameof(IsInModeGroupEditOrCreate));
            }
        }

        public bool IsInDefaultMode => 
            WorkingMode == WorkingMode.WorkingModeDefault;

        public bool IsInModeTaskView =>
            WorkingMode == WorkingMode.WorkingModeTaskVeiw;

        public bool IsInModeTaskEdit =>
            WorkingMode == WorkingMode.WorkingModeTaskEdit;

        public bool IsInModeTaskCreate =>
            WorkingMode == WorkingMode.WorkingModeTaskCreate;

        public bool IsInModeGroupVeiw =>
            WorkingMode == WorkingMode.WorkingModeGroupView;

        public bool IsInModeGroupEdit =>
            WorkingMode == WorkingMode.WorkingModeGroupEdit;

        public bool IsInModeGroupGreate =>
            WorkingMode == WorkingMode.WorkingModeGroupCreate;

        public bool IsInModeGroupViewOrEdit =>
            WorkingMode == WorkingMode.WorkingModeGroupView ||
            WorkingMode == WorkingMode.WorkingModeGroupEdit;

        public bool IsInModeGroupEditOrCreate =>            
                WorkingMode == WorkingMode.WorkingModeGroupEdit     ||
                WorkingMode == WorkingMode.WorkingModeGroupCreate   
                ;

        public bool IsNotInEditingOrCreating =>
            !(
                WorkingMode == WorkingMode.WorkingModeGroupEdit     ||
                WorkingMode == WorkingMode.WorkingModeGroupCreate   ||
                WorkingMode == WorkingMode.WorkingModeTaskEdit      ||
                WorkingMode == WorkingMode.WorkingModeTaskCreate
                );

        public bool IsWorkingWithGroup =>
            WorkingMode == WorkingMode.WorkingModeGroupView ||
            WorkingMode == WorkingMode.WorkingModeGroupEdit ||
            WorkingMode == WorkingMode.WorkingModeGroupCreate;

        public bool IsWorkingWithTask =>
            WorkingMode == WorkingMode.WorkingModeTaskVeiw ||
            WorkingMode == WorkingMode.WorkingModeTaskEdit ||
            WorkingMode == WorkingMode.WorkingModeTaskCreate;

        public RelayCommand RelayCommandGroupCreate { get; set; }
        public RelayCommand RelayCommandGroupEditSave { get; set; }
        public RelayCommand RelayCommandGroupSaveNew { get; set; }
        public RelayCommand RelayCommandGroupEdit { get; set; }
        public RelayCommand RelayCommandGroupCreateCancel { get; set; }
        public RelayCommand RelayCommandGroupEditCancel { get; set; }

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
                if (value)
                {
                    IsUserTasksCollectionSelected = false;
                    WorkingMode = WorkingMode.WorkingModeTaskVeiw;
                }
//                IsInProgressUserTaskCreating = (value) || IsInProgressUserTaskCreating ;
                
                OnPropertyChanged(nameof(IsUserTaskSelected));
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
                    WorkingMode = WorkingMode.WorkingModeGroupView;
                }
                OnPropertyChanged(nameof(IsUserTasksCollectionSelected));
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

        // Создание группы

        public void GroupCreate(object obj)
        {
            WorkingMode = WorkingMode.WorkingModeGroupCreate;
            var newGroup = new UserTasksCollection {GroupName = "Новая Группа"};
            SelectedUserTasksCollection = newGroup;
        }

        public void GroupCreateCancel(object obj)
        {
            SelectedUserTasksCollection = null;
            WorkingMode = WorkingMode.WorkingModeDefault;
        }

        public void GroupCreateSave(object obj)
        {
            GroupEditSave(obj);
            SuperCollectionTasks.Add(SelectedUserTasksCollection);

        }

        // Редактирование группы
        public void GroupEdit(object obj)
        {
            _oldGroupName = SelectedUserTasksCollection.GroupName;
            WorkingMode = WorkingMode.WorkingModeGroupEdit;
        }

        public void GroupEditSave(object obj)
        {
          //  SelectedUserTasksCollection.GroupName = (string) groupName;
            WorkingMode = WorkingMode.WorkingModeGroupView;
        }

        public void GroupEditCancel(object obj)
        {
            SelectedUserTasksCollection.GroupName = _oldGroupName;
            WorkingMode = WorkingMode.WorkingModeGroupView;
        }




















        //public void SaveNewGroup(UserTasksCollection newGroup)
        //{
        //    SuperCollectionTasks.Add(newGroup);
        //    WorkingMode = WorkingMode.WorkingModeUserTasksCollectionEdit;
        //}

        //public void NewGenericUserTaskCreate()
        //{
        //    WorkingMode = WorkingMode.WorkingModeUserTaskCreate;
        //    var newUserTask = new UserTask
        //    {
        //        Name = "Новая задача",
        //        Description = "",
        //        DueDate = DateTime.Today.AddDays(1),
        //        Status = Model.TaskStatus.New,
        //        Group = SelectedUserTasksCollection.GroupName
        //    };
        //    SelectedTask = newUserTask;

        //}

        //public void SaveNewUserTask(UserTask newUserTask)
        //{
        //    SelectedUserTasksCollection.Add(newUserTask);
        //    WorkingMode = WorkingMode.WorkingModeUserTaskEdit;
        //}

        //public void CancelNewUserTaskCreation()
        //{
        //    SelectedTask = null;
        //    WorkingMode = WorkingMode.WorkingModeDefault;
        //}

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
