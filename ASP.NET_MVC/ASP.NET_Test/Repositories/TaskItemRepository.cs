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
    public class TaskItemRepository : ITaskItemRepository
    {
        private ToDoContext db = null;

        public TaskItemRepository()
        {
            this.db = new ToDoContext();
        }

        public IEnumerable<TaskItem> GetAll(int taskId)
        {
            var task = db.Tasks.Find(taskId);
            if (task == null)
            {
                return null;
            }
            return task.TaskItems.ToList();
        }

        public TaskItem GetById(int taskItemId)
        {
            var taskItem = db.TaskItems.Find(taskItemId);
            if (taskItem != null)
            {
                return taskItem;
            }
            else
            {
                return null;
            }
        }

        public void Add(int taskId, TaskItem taskItem)
        {
            var task = db.Tasks.Find(taskId);
            if (task != null)
            {
                task.TaskItems.Add(taskItem);
            }
        }

        public void Edit(TaskItem taskItem)
        {
            if (taskItem != null)
            {
                db.Entry(taskItem).State = EntityState.Modified;
            }
        }

        public void Delete(int taskItemId)
        {
            var existedTaskItem = db.TaskItems.Find(taskItemId);
            if (existedTaskItem != null)
            {
                db.TaskItems.Remove(existedTaskItem);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void ChangeStatus(int taskItemId)
        {
            var taskItem = db.TaskItems.Where(t => t.TaskItemId == taskItemId).SingleOrDefault();
            if (taskItem != null)
            {
                if (taskItem.Status == false)
                {
                    taskItem.Status = true;
                }
                else
                {
                    taskItem.Status = false;
                }
                db.Entry(taskItem).State = EntityState.Modified;
            }
        }
    }
}