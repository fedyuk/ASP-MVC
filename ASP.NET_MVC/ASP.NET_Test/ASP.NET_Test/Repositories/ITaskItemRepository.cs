using ASP.NET_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASP.NET_Test.Repositories
{
    interface ITaskItemRepository
    {
        IEnumerable<TaskItem> GetAll(int taskId);
        TaskItem GetById(int taskItemId);
        void Add(int taskId, TaskItem taskItem);
        void Edit(TaskItem taskItem);
        void Delete(int taskItemId);
        void Save();
        void ChangeStatus(int taskItemId);
    }
}
