using ASP.NET_Test.Models;
using ASP.NET_Test.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository repository = null;

        public AccountService()
        {
            this.repository = new AccountRepository();
        }

        public IEnumerable<Account> GetAll()
        {
            return repository.GetAll();
        }

        public Account GetById(int accountId)
        {
            return repository.GetById(accountId);
        }

        public void Add(Account account)
        {
            repository.Add(account);
        }

        public void Edit(Account account)
        {
            repository.Edit(account);
        }

        public void Delete(int accountId)
        {
            repository.Delete(accountId);
        }

        public void Save()
        {
            repository.Save();
        }

        public bool IsExists(Account account)
        {
            return repository.IsExists(account);
        }

        public Account GetByEmailAndPassword(Account account)
        {
            return repository.GetByEmailAndPassword(account);
        }

        public Account GetByEmail(String email)
        {
            return repository.GetByEmail(email);
        }

        public Account GetByPassword(String password)
        {
            return repository.GetByPassword(password);
        }
    }
}