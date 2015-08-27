using ASP.NET_Test.Models;
using ASP.NET_Test.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.Services
{
    public class TaskItemService : ITaskItemService
    {
        private ITaskItemRepository repository = null;

        public TaskItemService()
        {
            this.repository = new TaskItemRepository();
        }

        public IEnumerable<TaskItem> GetAll(int taskId)
        {
            return repository.GetAll(taskId);
        }

        public TaskItem GetById(int taskItemId)
        {
            return repository.GetById(taskItemId);
        }

        public void Add(int taskId, TaskItem taskItem)
        {
            repository.Add(taskId, taskItem);
        }

        public void Edit(TaskItem taskItem)
        {
            repository.Edit(taskItem);
        }

        public void Delete(int taskItemId)
        {
            repository.Delete(taskItemId);
        }

        public void Save()
        {
            repository.Save();
        }

        public void ChangeStatus(int taskItemId)
        {
            repository.ChangeStatus(taskItemId);
        }
    }
}