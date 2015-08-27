using ASP.NET_Test.DataBaseInitialization;
using ASP.NET_Test.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private ToDoContext db = null;

        public TaskRepository()
        {
            this.db = new ToDoContext();
        }

        public IEnumerable<Task> GetAll(int categoryId)
        {
            var category = db.Categories.Find(categoryId);
            if (category != null)
            {
                return category.Tasks.ToList();
            }
            else
            {
                return null;
            }
        }

        public Task GetById(int taskId)
        {
            var task = db.Tasks.Find(taskId);
            if (task != null)
            {
                return task;
            }
            else
            {
                return null;
            }
        }

        public void Add(int categoryId, Task task)
        {
            var category = db.Categories.Find(categoryId);
            if (category != null)
            {
                if (task != null)
                {
                    category.Tasks.Add(task);
                }
            }
        }

        public void Edit(Task task)
        {
            if (task != null)
            {
                db.Entry(task).State = EntityState.Modified;
            }
        }

        public void Delete(int taskId)
        {
            
            var existedTask = db.Tasks.Find(taskId);
            if (existedTask != null)
            {
                var taskItems = existedTask.TaskItems.ToList();
                foreach (var taskItem in taskItems)
                {
                    db.TaskItems.Remove(taskItem);
                }
                db.Tasks.Remove(existedTask);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public int GetCountTaskItemsByTaskId(int taskId)
        {
            var task = db.Tasks.Find(taskId);
            if (task != null)
            {
                return task.TaskItems.Count();
            }
            else
            {
                return 0;
            }
        }
    }
}