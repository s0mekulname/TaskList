using System.ComponentModel;

namespace Model
{
    // Режимы работы приложения
    public enum WorkingMode
    {
        // Ничего не выбрано
        [Description("Ничего не выбрано")]
        WorkingModeDefault = 0,

        // Обзор задачи
        [Description("Обзор задачи")]
        WorkingModeTaskVeiw = 11,

        // Редактирование задачи
        [Description("Редактирование задачи")]
        WorkingModeTaskEdit = 12,

        // Создание новой задачи
        [Description("Создание новой задачи")]
        WorkingModeTaskCreate = 13,

        // Обзор группы
        [Description("Обзор группы")]
        WorkingModeGroupView = 21,

        // Редактирование группы
        [Description("Редактирование группы")]
        WorkingModeGroupEdit = 22,

        // Создание новой группы
        [Description("Создание новой группы")]
        WorkingModeGroupCreate = 23
    }
}
