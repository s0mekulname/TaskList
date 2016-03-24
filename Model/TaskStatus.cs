using System.ComponentModel.DataAnnotations;

namespace Model
{
    public enum TaskStatus
    {
        [Display(Name = "Новая")]
        New = 1,

        [Display(Name = "Выполняется")]
        InProgress = 2,

        [Display(Name = "Отложена")]
        Postponded = 3,

        [Display(Name = "Завершена")]
        Completed = 4,

        [Display(Name = "Отменена")]
        Canceled = 5
    }
}
