﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Model;
using TaskStatus = Model.TaskStatus;

namespace ValueConverters
{
    public class TaskStatusToStringConverter: IValueConverter
    {
        private static string GetTaskStatusImage(TaskStatus taskStatus)
        {
            switch (taskStatus)
            {
                case TaskStatus.New         : return "Images/blue_dot.png";
                case TaskStatus.Completed   : return "Images/green_dot.png";
                case TaskStatus.Canceled    : return "Images/red_dot.png";
                case TaskStatus.InProgress  : return "Images/violet_dot.png";
                case TaskStatus.Postponded  : return "Images/magenta_dot.png";
                default:throw new Exception("Unexpected Task Status");
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string) GetTaskStatusImage((TaskStatus)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Illegal string to task status conversion");
        }
    }
}
