using ASP.NET_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASP.NET_Test.Repositories
{
    interface ITaskRepository
    {
        IEnumerable<Task> GetAll(int categoryId);
        Task GetById(int taskId);
        void Add(int categoryId, Task task);
        void Edit(Task task);
        void Delete(int taskId);
        void Save();
        int GetCountTaskItemsByTaskId(int taskId);
    }
}
