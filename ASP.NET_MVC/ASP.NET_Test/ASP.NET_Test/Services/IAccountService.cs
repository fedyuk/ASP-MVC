using ASP.NET_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASP.NET_Test.Services
{
    interface IAccountService
    {
        IEnumerable<Account> GetAll();
        Account GetById(int accountId);
        void Add(Account account);
        void Edit(Account account);
        void Delete(int accountId);
        void Save();
        bool IsExists(Account account);
        Account GetByEmailAndPassword(Account account);
        Account GetByEmail(String email);
        Account GetByPassword(String password);
    }
}
