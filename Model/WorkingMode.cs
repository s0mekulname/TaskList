using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum WorkingMode
    {
       WorkingModeDefault = 0,
       WorkingModeUserTaskEdit = 1,
       WorkingModeUserTaskCreate = 2,
       WorkingModeUserTasksCollectionEdit = 3,
       WorkingModeUserTasksCollectionCreate = 4
    }
}
