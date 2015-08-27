using ASP.NET_Test.Models;
using ASP.NET_Test.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository repository = null;

        public CategoryService()
        {
            this.repository = new CategoryRepository();
        }

        public IEnumerable<Category> GetAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<Category> GetAllByAccountId(int accountId)
        {
            return repository.GetAllByAccountId(accountId);
        }

        public Category GetById(int categoryId)
        {
            return repository.GetById(categoryId);
        }

        public void Add(int accountId, Category category)
        {
            repository.Add(accountId, category);
        }

        public void Edit(Category category)
        {
            repository.Edit(category);
        }

        public void Delete(int categoryId)
        {
            repository.Delete(categoryId);
        }

        public void Save()
        {
            repository.Save();
        }

        public int GetCountTaskByCategoryId(int categoryId)
        {
            return repository.GetCountTaskByCategoryId(categoryId);
        }
    }
}