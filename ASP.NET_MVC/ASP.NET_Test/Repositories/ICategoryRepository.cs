using ASP.NET_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASP.NET_Test.Repositories
{
    interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetAllByAccountId(int accountId);
        Category GetById(int categoryId);
        void Add(int accountId, Category category);
        void Edit(Category category);
        void Delete(int categoryId);
        void Save();
        int GetCountTaskByCategoryId(int categoryId);
    }
}
