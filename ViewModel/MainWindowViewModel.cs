using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
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
         

            #region UserTasks Initialization 2

            AllUserTasks = new TaskGroup
            {
                UserTasks = new ObservableCollection<UserTask>
                {

                    // Новые задачи
                    new UserTask
                    {
                        Name = "Новая задача 1",
                        Description = "Новая задача 1 должна быть " +
                                      "выполнена до 1 декабря 2016 года",
                        DueDate = Convert.ToDateTime("2016-12-1"),
                        Status = TaskStatus.New,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Новая задача 3 Истекает завтра",
                        Description = "Новая задача 3 должна быть " +
                                      "выполнена до завтра",
                        DueDate = DateTime.Today.AddDays(1),
                        Status = TaskStatus.New,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Новая задача 4 Истекает сегодня",
                        Description = "Новая задача 4 должна быть " +
                                      "выполнена сегодня",
                        DueDate = DateTime.Today,
                        Status = TaskStatus.New,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Новая задача 5 сразу просрочена",
                        Description = "Новая задача 5 " +
                                      "просрочена ещё вчера",
                        DueDate = DateTime.Today.AddDays(-1),
                        Status = TaskStatus.New,
                        Group = "Default"
                    },

                    // Выполняющиеся задачи
                    new UserTask
                    {
                        Name = "Выполняющаяся задача 1",
                        Description = "Выполняющаяся задача 1 должна быть " +
                                      "выполнена до 1 декабря 2016 года" +
                                      "и всё ещё выполняется",
                        DueDate = Convert.ToDateTime("2016-12-1"),
                        Status = TaskStatus.InProgress,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Выполняющаяся задача 3 Истекает завтра",
                        Description = "Выполняющаяся задача 3 должна быть " +
                                      "выполнена до завтра" +
                                      "и всё ещё выполняется",
                        DueDate = DateTime.Today.AddDays(1),
                        Status = TaskStatus.InProgress,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Выполняющаяся задача 4 Истекает сегодня",
                        Description = "Выполняющаяся задача 4 должна быть " +
                                      "выполнена сегодня" +
                                      "и всё ещё выполняется",
                        DueDate = DateTime.Today,
                        Status = TaskStatus.InProgress,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Выполняющаяся задача 5 сразу просрочена",
                        Description = "Выполняющаяся задача 5 " +
                                      "просрочена ещё вчера" +
                                      "и всё ещё выполняется",
                        DueDate = DateTime.Today.AddDays(-1),
                        Status = TaskStatus.InProgress,
                        Group = "Default"
                    },



                    // Отложенные задачи
                    new UserTask
                    {
                        Name = "Отложенная задача 1",
                        Description = "Отложенная задача 1 должна быть " +
                                      "выполнена до 1 декабря 2016 года" +
                                      "но пока отложена",
                        DueDate = Convert.ToDateTime("2016-12-1"),
                        Status = TaskStatus.Postponded,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Отложенная задача 3 Истекает завтра",
                        Description = "Отложенная задача 3 должна быть " +
                                      "выполнена до завтра" +
                                      "но пока отложена",
                        DueDate = DateTime.Today.AddDays(1),
                        Status = TaskStatus.Postponded,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Отложенная задача 4 Истекает сегодня",
                        Description = "Отложенная задача 4 должна быть " +
                                      "выполнена сегодня" +
                                      "но пока отложена",
                        DueDate = DateTime.Today,
                        Status = TaskStatus.Postponded,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Отложенная задача 5 сразу просрочена",
                        Description = "Отложенная задача 5 " +
                                      "должна была быть выполнена ещё вчера" +
                                      "но была отложена и до сих пор не выполнена",
                        DueDate = DateTime.Today.AddDays(-1),
                        Status = TaskStatus.Postponded,
                        Group = "Default"
                    },                    
                    
                    // Завершенные задачи
                    new UserTask
                    {
                        Name = "Завершённая задача 1 уже выполнена",
                        Description = "Завершённая задача 1 должна была быть " +
                                      "выполнена до 1 декабря 2016 года и уже выполнена",
                        DueDate = Convert.ToDateTime("2016-12-1"),
                        Status = TaskStatus.Completed,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Завершённая задача 3 Истекает завтра",
                        Description = "Завершённая задача 3 должна была быть " +
                                      "выполнена до завтра и уже выполнена",
                        DueDate = DateTime.Today.AddDays(1),
                        Status = TaskStatus.Completed,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Завершённая задача 4 Истекает сегодня",
                        Description = "Завершённая задача 4 должна была быть " +
                                      "выполнена сегодня и она уже выполнена",
                        DueDate = DateTime.Today,
                        Status = TaskStatus.Completed,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Завершённая задача 5  просрочена",
                        Description = "Завершённая задача 5 " +
                                      "должна была быть выполнена ещё позавчера " +
                                      "но была выполнена позже (вчера)",
                        DueDate = DateTime.Today.AddDays(-2),
                        Status = TaskStatus.Completed,
                        Group = "Default"
                    },

                    // Отменённые задачи
                    new UserTask
                    {
                        Name = "Отменённая задача 1 истекает в будущем",
                        Description = "Отменённая задача 1 должна была быть " +
                                      "выполнена до 1 декабря 2016 года " +
                                      "но была досрочно отменена",
                        DueDate = Convert.ToDateTime("2016-12-1"),
                        Status = TaskStatus.Canceled,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Отменённая задача 3 Истекает завтра",
                        Description = "Отменённая задача 3 должна была быть " +
                                      "выполнена до завтра " +
                                      "но была досрочно отменена",
                        DueDate = DateTime.Today.AddDays(1),
                        Status = TaskStatus.Canceled,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Отменённая задача 4 Истекает сегодня",
                        Description = "Отменённая задача 4 должна была быть " +
                                      "выполнена сегодня "+
                                      "но была досрочно отменена",
                        DueDate = DateTime.Today,
                        Status = TaskStatus.Canceled,
                        Group = "Default"
                    },

                    new UserTask
                    {
                        Name = "Отменённая задача 5",
                        Description = "Отменённая задача 5 " +
                                      "должна была быть выполнена ещё вчера " +
                                      "но была досрочно отменена (позавчера)",
                        DueDate = DateTime.Today.AddDays(-1),
                        Status = TaskStatus.Canceled,
                        Group = "Default"
                    },


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
                ExpiredTasks,
                ExpireTomorrowTasks,
                DefaultTasks

            };

            IsShowCompleted = true;
            IsShowCanceled = true;
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
        private bool _isShowCompleted;
        private bool _isShowCanceled;

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


        public bool IsShowCompleted
        {
            get { return _isShowCompleted; }
            set
            {
                _isShowCompleted = value;
                FilterFunction();
                OnPropertyChanged(nameof(IsShowCompleted));
                OnPropertyChanged(nameof(IsShowCanceled));
            }
        }

        public bool IsShowCanceled
        {
            get { return _isShowCanceled; }
            set
            {
                _isShowCanceled = value;
                FilterFunction();
                OnPropertyChanged(nameof(IsShowCanceled));
                OnPropertyChanged(nameof(IsShowCompleted));
            }
        }



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


        public void FilterFunction()
        {
            if (SuperCollectionTasks == null) return;
            foreach (TaskGroup taskGroup in SuperCollectionTasks)
            {
                ICollectionView userTaskDataSourceView = 
                    CollectionViewSource.GetDefaultView(taskGroup.UserTasks);
                if (IsShowCompleted && IsShowCanceled)
                {
                    userTaskDataSourceView.Filter = userTask => true;
                }
                if (IsShowCompleted && !IsShowCanceled)
                {
                    userTaskDataSourceView.Filter = userTask => 
                        ((UserTask)userTask).Status != TaskStatus.Canceled;
                }
                if (!IsShowCompleted && IsShowCanceled)
                {
                    userTaskDataSourceView.Filter = userTask => 
                        ((UserTask)userTask).Status != TaskStatus.Completed;
                }
                if (!IsShowCompleted && !IsShowCanceled)
                {
                    userTaskDataSourceView.Filter = userTask => 
                        ((UserTask)userTask).Status != TaskStatus.Completed &&
                        ((UserTask)userTask).Status != TaskStatus.Canceled;
                }
            }

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
            bool isDateChanged = _oldSelectedUserTask.DueDate.Day != SelectedTask.DueDate.Day;
            if (isDateChanged)
            {
                _oldSelectedUserTask.DueDate = SelectedTask.DueDate;
            }
            bool isTaskStatusChanged = _oldSelectedUserTask.Status != SelectedTask.Status;
            if (isTaskStatusChanged)
            {
                _oldSelectedUserTask.Status = SelectedTask.Status;
            }

            _oldSelectedUserTask.Status = SelectedTask.Status;
            _oldSelectedUserTask.Group = SelectedTask.Group;

            SelectedTask = _oldSelectedUserTask;
            if (isDateChanged)
            {
                AutoGroupDistribution(SelectedTask);
            }
            if (isTaskStatusChanged)
            {
                AutoGroupDistribution(SelectedTask);
                FilterFunction();
            }

            _oldSelectedUserTask = null;
            WorkingMode = WorkingMode.WorkingModeTaskVeiw;
        }

        public void AutoGroupDistribution(UserTask userTask)
        {
            var dueDate = userTask.DueDate;
            var status = userTask.Status;
            var delta = (dueDate - DateTime.Today).Days;

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
       //     FilterFunction();

        }
}
}
