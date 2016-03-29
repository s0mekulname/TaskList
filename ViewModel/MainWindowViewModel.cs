using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;
using ViewModel.Annotations;
using TaskStatus = Model.TaskStatus;

namespace ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private UserTask _selectedTask;
        private TaskGroup _selectedTaskGroup;
        private bool _isUserTaskSelected;
        private bool _isUserTasksCollectionSelected;
        private WorkingMode _workingMode;
        public TaskGroup AllUserTasks { get; set; }

        public TaskGroup ExpiredTasks { get; set; }

        public TaskGroup ExpireTomorrowTasks { get; set; }

        public TaskGroup DefaultTasks { get; set; }

        // Иерархическая коллекция всех групп
        public ObservableCollection<TaskGroup> SuperCollectionTasks { get; set; }

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

            AllUserTasks = new TaskGroup
            {
                UserTasks = new ObservableCollection<UserTask>
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
                },
                IsAutoGroup = false,
                GroupName = "All"
            };

            #endregion

            ExpiredTasks = new TaskGroup()
            {
                GroupName = "Expired",
                IsAutoGroup = true
            };

            ExpireTomorrowTasks = new TaskGroup()
            {
                GroupName = "Expire Tomorrow",
                IsAutoGroup = true
            };

            DefaultTasks = new TaskGroup()
            {
                GroupName = "Default",
                IsAutoGroup = false

            };

            // Добавляем элемнты из UserTasks в DefaultTasks
            // TODO: (Должно быть лучшее решение для копирования)
            foreach (var u in AllUserTasks.UserTasks)
            {
                DefaultTasks.UserTasks.Add(u);
                AutoGroupDistribution(u);
            }

            SuperCollectionTasks = new ObservableCollection<TaskGroup>
            {
                DefaultTasks,
                ExpiredTasks,
                ExpireTomorrowTasks
            };
            SelectedTask = null;
            WorkingMode = WorkingMode.WorkingModeDefault;
            SelectedTaskGroup = null;
            IsUserTaskSelected = false;
            IsUserTasksCollectionSelected = false;

            _oldSelectedGroup = null;
            _oldSelectedUserTask = null;

            // Поле для хранения старого значения
            // имени группы (нужно для команды редактирования)
            //_oldGroupName = "";

            // Создать новую группу
            // Создать группу можно, если не включен режим 
            // редактирование или создания новой группы или задачи
            RelayCommandGroupCreate = 
                new RelayCommand(GroupCreate, param => IsNotInEditingOrCreating);

            // Сохранить созданную группу
            // Можно в режиме создания группы
            RelayCommandGroupCreateSave =
                new RelayCommand(GroupCreateSave, param => IsInModeGroupCreate);

            // Отменить создание группы
            // Можно в режиме создания группы
            RelayCommandGroupCreateCancel =
                new RelayCommand(GroupCreateCancel, param => IsInModeGroupCreate);

            // Редактирование группу
            // Начать редактировать группу можно из режима обзора и если
            // выбранная группа не является автогруппой
            RelayCommandGroupEdit = 
                new RelayCommand(GroupEdit, param => IsGroupEditAvailable );

            // Отменить редактирование группы
            // Можно в режиме редактирования
            RelayCommandGroupEditCancel = 
                new RelayCommand(GroupEditCancel, param => IsInModeGroupEdit);

            // Сохранить изменения в группе
            // Можно в режиме редактирования
            RelayCommandGroupEditSave =
                new RelayCommand(GroupEditSave, param => IsInModeGroupEdit);


            // задачи

            // Создать новую задачу
            // Создать задачу можно в режиме просмотра группы
            RelayCommandTaskCreate =
                new RelayCommand(TaskCreate, param => IsInModeGroupVeiw);

            // Сохранить созданную задачу
            // Можно в режиме создания задачи
            RelayCommandTaskCreateSave =
                new RelayCommand(TaskCreateSave, param => IsInModeTaskCreate);

            // Отменить создание задачи
            // Можно в режиме создания задачи
            RelayCommandTaskCreateCancel =
                new RelayCommand(TaskCreateCancel, param => IsInModeTaskCreate);

            // Редактирование задачи
            // Начать редактировать задачу можно из режима обзора задачи
            RelayCommandTaskEdit =
                new RelayCommand(TaskEdit, param => IsInModeTaskView);

            // Отменить редактирование задачи
            // Можно в режиме редактирования задачи
            RelayCommandTaskEditCancel =
                new RelayCommand(TaskEditCancel, param => IsInModeTaskEdit);

            // Сохранить изменения в задаче
            // Можно в режиме редактирования задачи
            RelayCommandTaskEditSave =
                new RelayCommand(TaskEditSave, param => IsInModeTaskEdit);





            //IsWorkingWithSelectedTask = false;
        }

        //private string _oldGroupName;

        private UserTask _oldSelectedUserTask;

        //private string _oldTaskName;
        //private string _oldTaskDescription;
        //private DateTime _oldTaskDueDate;
        //private Model.TaskStatus _oldTaskStatus;
        //private string _oldTaskGroup;

        private TaskGroup _oldSelectedGroup;

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
                OnPropertyChanged(nameof(IsInModeGroupCreate));

                OnPropertyChanged(nameof(IsWorkingWithGroup));
                OnPropertyChanged(nameof(IsWorkingWithTask));
                OnPropertyChanged(nameof(IsInModeGroupViewOrEdit));
                OnPropertyChanged(nameof(IsInModeTaskViewOrEdit));
                OnPropertyChanged(nameof(IsNotInEditingOrCreating));
                OnPropertyChanged(nameof(IsInModeGroupEditOrCreate));
                OnPropertyChanged(nameof(IsInModeTaskEditOrCreate));
                OnPropertyChanged(nameof(IsGroupEditAvailable));
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

        public bool IsInModeGroupCreate =>
            WorkingMode == WorkingMode.WorkingModeGroupCreate;

        public bool IsInModeGroupViewOrEdit =>
            WorkingMode == WorkingMode.WorkingModeGroupView ||
            WorkingMode == WorkingMode.WorkingModeGroupEdit;

        public bool IsInModeTaskViewOrEdit =>
            WorkingMode == WorkingMode.WorkingModeTaskVeiw ||
            WorkingMode == WorkingMode.WorkingModeTaskEdit;

        public bool IsInModeGroupEditOrCreate =>            
                WorkingMode == WorkingMode.WorkingModeGroupEdit     ||
                WorkingMode == WorkingMode.WorkingModeGroupCreate   
                ;

        public bool IsInModeTaskEditOrCreate =>            
                WorkingMode == WorkingMode.WorkingModeTaskEdit     ||
                WorkingMode == WorkingMode.WorkingModeTaskCreate   
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

        public bool IsGroupEditAvailable =>
            WorkingMode == WorkingMode.WorkingModeGroupView &&
            !SelectedTaskGroup.IsAutoGroup; 

        public RelayCommand RelayCommandGroupCreate { get; set; }
        public RelayCommand RelayCommandGroupCreateSave { get; set; }
        public RelayCommand RelayCommandGroupCreateCancel { get; set; }

        public RelayCommand RelayCommandGroupEdit { get; set; }
        public RelayCommand RelayCommandGroupEditSave { get; set; }       
        public RelayCommand RelayCommandGroupEditCancel { get; set; }

        public RelayCommand RelayCommandTaskCreate { get; set; }
        public RelayCommand RelayCommandTaskCreateSave { get; set; }
        public RelayCommand RelayCommandTaskCreateCancel { get; set; }

        public RelayCommand RelayCommandTaskEdit { get; set; }
        public RelayCommand RelayCommandTaskEditSave { get; set; }
        public RelayCommand RelayCommandTaskEditCancel { get; set; }

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
                
                OnPropertyChanged(nameof(IsUserTaskSelected));
            }
        }


        public TaskGroup SelectedTaskGroup
        {
            get { return _selectedTaskGroup; }
            set
            {
                _selectedTaskGroup = value;
                
                IsUserTasksCollectionSelected = (value != null);
                OnPropertyChanged(nameof(SelectedTaskGroup));
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
            var newUserTasksCollection = new TaskGroup {GroupName = name};
            SuperCollectionTasks.Add(newUserTasksCollection);

        }

        // Создание группы

        public void GroupCreate(object obj)
        {
            
            var newGroup = new TaskGroup
            {
                GroupName = "Новая Группа",
                IsAutoGroup = false
            };
            SelectedTaskGroup = newGroup;
            WorkingMode = WorkingMode.WorkingModeGroupCreate;
        }

        public void GroupCreateCancel(object obj)
        {
            SelectedTaskGroup = null;
            WorkingMode = WorkingMode.WorkingModeDefault;
        }

        public void GroupCreateSave(object obj)
        {
            
            SuperCollectionTasks.Add(SelectedTaskGroup);
            WorkingMode = WorkingMode.WorkingModeGroupView;
        }

        // Редактирование группы
        public void GroupEdit(object obj)
        {
            _oldSelectedGroup = SelectedTaskGroup;

            var tempGroup = new TaskGroup
            {
                GroupName = _oldSelectedGroup.GroupName
            };

            SelectedTaskGroup = tempGroup;

            WorkingMode = WorkingMode.WorkingModeGroupEdit;
        }

        public void GroupEditSave(object obj)
        {
            _oldSelectedGroup.GroupName = SelectedTaskGroup.GroupName;
            SelectedTaskGroup = _oldSelectedGroup;
            _oldSelectedGroup = null;
            WorkingMode = WorkingMode.WorkingModeGroupView;
        }

        public void GroupEditCancel(object obj)
        {
            SelectedTaskGroup = _oldSelectedGroup;
            _oldSelectedGroup = null;
            WorkingMode = WorkingMode.WorkingModeGroupView;
        }

        public void TaskCreate(object obj)
        {
            var newUserTask = new UserTask
            {
                Name = "Новая задача",
                Description = "Добавьте описание",
                DueDate = DateTime.Today.AddDays(1),
                Status = Model.TaskStatus.New,
                Group = SelectedTaskGroup.GroupName
            };

            SelectedTask = newUserTask;
            WorkingMode = WorkingMode.WorkingModeTaskCreate;
        }

        public void TaskCreateSave(object obj)
        {
            SelectedTaskGroup.UserTasks.Add(SelectedTask);
            AutoGroupDistribution(SelectedTask);
            WorkingMode = WorkingMode.WorkingModeTaskVeiw;           
        }

        public void TaskCreateCancel(object obj)
        {
            SelectedTask = null;
            WorkingMode = WorkingMode.WorkingModeGroupView;
        }

        public void TaskEdit(object obj)
        {
            _oldSelectedUserTask = SelectedTask;

            var tempTask = new UserTask
            {
                Name = _oldSelectedUserTask.Name,
                Description = _oldSelectedUserTask.Description,
                DueDate = _oldSelectedUserTask.DueDate,
                Status = _oldSelectedUserTask.Status,
                Group = _oldSelectedUserTask.Group
            };

            SelectedTask = tempTask;


            WorkingMode = WorkingMode.WorkingModeTaskEdit;
        }

        public void TaskEditCancel(object obj)
        {
            SelectedTask = _oldSelectedUserTask;
            _oldSelectedUserTask = null;
            WorkingMode = WorkingMode.WorkingModeTaskVeiw;
        }

        public void TaskEditSave(object obj)
        {
            _oldSelectedUserTask.Name = SelectedTask.Name;
            _oldSelectedUserTask.Description = SelectedTask.Description;
            bool isDateChanged = _oldSelectedUserTask.DueDate.Day == SelectedTask.DueDate.Day;
            if (isDateChanged)
            {
                _oldSelectedUserTask.DueDate = SelectedTask.DueDate;
            }

            _oldSelectedUserTask.Status = SelectedTask.Status;
            _oldSelectedUserTask.Group = SelectedTask.Group;

            SelectedTask = _oldSelectedUserTask;
            if (isDateChanged)
            {
                AutoGroupDistribution(SelectedTask);
            }
            _oldSelectedUserTask = null;
            WorkingMode = WorkingMode.WorkingModeTaskVeiw;
        }

        public void AutoGroupDistribution(UserTask userTask)
        {
            var dueDate = userTask.DueDate;
            var status = userTask.Status;
            var delta = dueDate.Day - DateTime.Today.Day;

            bool isExpired =
                status != TaskStatus.Canceled &&
                status != TaskStatus.Completed &&
                delta < 0;

            if (isExpired)
            {
                ExpiredTasks.UserTasks.Add(userTask);
            }
            else if (ExpiredTasks.UserTasks.Contains(userTask))
            {
                ExpiredTasks.UserTasks.Remove(userTask);
            }

            bool isExpireTomorrow =
                status != TaskStatus.Canceled &&
                status != TaskStatus.Completed &&
                (delta == 0 || delta == 1);

            if (isExpireTomorrow)
            {
                ExpireTomorrowTasks.UserTasks.Add(userTask);
            }
            else if (ExpireTomorrowTasks.UserTasks.Contains(userTask))
            {
                ExpireTomorrowTasks.UserTasks.Remove(userTask);
            }

        }
    }
}
