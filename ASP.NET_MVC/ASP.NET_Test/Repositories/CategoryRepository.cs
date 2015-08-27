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
    public class CategoryRepository : ICategoryRepository
    {
        private ToDoContext db = null;

        public CategoryRepository()
        {
            this.db = new ToDoContext();
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = db.Categories.ToList();
            if (categories != null)
            {
                return categories;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Category> GetAllByAccountId(int accountId)
        {
            var account = db.Accounts.Find(accountId);
            if (account != null)
            {
                return account.Categories.ToList();
            }
            else
            {
                return null;
            }
        }

        public Category GetById(int categoryId)
        {
            var category = db.Categories.Find(categoryId);
            if (category != null)
            {
                return category;
            }
            else
            {
                return null;
            }
        }

        public void Add(int accountId, Category category)
        {
            var account = db.Accounts.Find(accountId);
            if (account != null)
            {
                if (category != null)
                {
                    account.Categories.Add(category);
                }
            }
        }

        public void Edit(Category category)
        {
            if (category != null)
            {
                db.Entry(category).State = EntityState.Modified;
            }
        }

        public void Delete(int categoryId)
        {
            var existedCategory = db.Categories.Find(categoryId);
            if (existedCategory != null)
            {
                var tasks = existedCategory.Tasks.ToList();
                foreach (var task in tasks)
                {
                    var existedTask = db.Tasks.Find(task.TaskId);
                    var taskItems = existedTask.TaskItems.ToList();
                    foreach (var taskItem in taskItems)
                    {
                        db.TaskItems.Remove(taskItem);
                    }
                    db.Tasks.Remove(task);
                }
                db.Categories.Remove(existedCategory);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public int GetCountTaskByCategoryId(int categoryId)
        {
            var category = db.Categories.Find(categoryId);
            if (category != null)
            {
                return category.Tasks.Count();
            }
            else
            {
                return 0;
            }
        }
    }
}