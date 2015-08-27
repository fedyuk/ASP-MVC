using ASP.NET_Test.Models;
using ASP.NET_Test.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.Services
{
    public class TaskService : ITaskService
    {
        private ITaskRepository repository = null;

        public TaskService()
        {
            this.repository = new TaskRepository();
        }

        public IEnumerable<Task> GetAll(int categoryId)
        {
            return repository.GetAll(categoryId);
        }

        public Task GetById(int taskId)
        {
            return repository.GetById(taskId);
        }

        public void Add(int categoryId, Task task)
        {
            repository.Add(categoryId, task);
        }

        public void Edit(Task task)
        {
            repository.Edit(task);
        }

        public void Delete(int taskId)
        {
            repository.Delete(taskId);
        }

        public void Save()
        {
            repository.Save();
        }

        public int GetCountTaskItemsByTaskId(int taskId)
        {
            return repository.GetCountTaskItemsByTaskId(taskId);
        }
    }
}