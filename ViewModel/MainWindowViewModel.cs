using System;
using System.Collections;
using System.Collections.ObjectModel;
using Model;
using TaskStatus = Model.TaskStatus;

namespace ViewModel
{

    public class UserTasksCollection : ObservableCollection<UserTask>
    {
        public string GroupName { get; set; }
    }

    public class MainWindowViewModel
    {
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
        }

        public UserTask SelectedTask { get; set; }
    }
}
