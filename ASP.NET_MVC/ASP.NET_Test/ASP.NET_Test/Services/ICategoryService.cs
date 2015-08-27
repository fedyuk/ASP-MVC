using ASP.NET_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASP.NET_Test.Services
{
    interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int categoryId);
        IEnumerable<Category> GetAllByAccountId(int accountId);
        void Add(int accountId, Category category);
        void Edit(Category category);
        void Delete(int categoryId);
        void Save();
        int GetCountTaskByCategoryId(int categoryId);
    }
}
