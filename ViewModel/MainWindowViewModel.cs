using System;
using System.Collections.ObjectModel;
using Model;
using TaskStatus = Model.TaskStatus;

namespace ViewModel
{
    public class MainWindowViewModel
    {
        public ObservableCollection <UserTask> UserTasks { get; set; }

        public MainWindowViewModel()
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
                    Name = " Canceled Task 6",
                    Description = "Description 1",
                    DueDate = Convert.ToDateTime("2017-03-30"),
                    Status = TaskStatus.Canceled,
                    Group = "Default"
                }
            };
        }
         
        public UserTask SelectedTask { get; set; }
    }
}
