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
    public class AccountRepository : IAccountRepository
    {
        private ToDoContext db = null;

        public AccountRepository()
        {
            this.db = new ToDoContext();
        }

        public IEnumerable<Account> GetAll()
        {
            var accounts = db.Accounts.ToList();
            if (accounts != null)
            {
                return accounts;
            }
            else
            {
                return null;
            }
        }

        public Account GetById(int accountId)
        {
            var account = db.Accounts.Find(accountId);
            if (account != null)
            {
                return account;
            }
            else
            {
                return null;
            }
        }

        public void Add(Account account)
        {
            if (account != null)
            {
                db.Accounts.Add(account);
            }
        }

        public void Edit(Account account)
        {
            if (account != null)
            {
                db.Entry(account).State = EntityState.Modified;
            }
        }

        public void Delete(int accountId)
        {
            var existedAccount = db.Accounts.Find(accountId);
            if (existedAccount != null)
            {
                db.Accounts.Remove(existedAccount);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public bool IsExists(Account account)
        {
            var accounts = db.Accounts.Where(a => a.Email == account.Email && a.Password == account.Password).FirstOrDefault();
            return accounts != null ? true : false;
        }

        public Account GetByEmailAndPassword(Account account)
        {
            var existedAccount = db.Accounts.Where(a => a.Email == account.Email && a.Password == account.Password).FirstOrDefault();
            if (existedAccount != null)
            {
                return existedAccount;
            }
            else
            {
                return null;
            }
        }

        public Account GetByEmail(String email)
        {
            var existedAccount = db.Accounts.Where(a => a.Email == email).FirstOrDefault();
            if (existedAccount != null)
            {
                return existedAccount;
            }
            else
            {
                return null;
            }
        }

        public Account GetByPassword(String password)
        {
            var existedAccount = db.Accounts.Where(a => a.Password == password).FirstOrDefault();
            if (existedAccount != null)
            {
                return existedAccount;
            }
            else
            {
                return null;
            }
        }
    }
}