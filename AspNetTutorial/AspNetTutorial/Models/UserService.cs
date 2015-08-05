using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AspNetTutorial.Models
{
    public class UserService: IUserService
    {
        private UserNoteContext db;

        public UserService()
        {
            db = new UserNoteContext();
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User GetById(int id)
        {
            return db.Users.Find(id);
        }

        public User GetByUserNameAndPassword(String name, String password)
        {
            return db.Users.Where(a => a.Name.Equals(name) && a.Password.Equals(password)).FirstOrDefault();
        }

        public User GetByUserName(String name)
        {
            return db.Users.Where(a => a.Name.Equals(name)).FirstOrDefault();
        }

        public User Add(User user)
        {
            return db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
        }

        public bool IsExists(User user)
        {
            var users = db.Users.Where(a => a.Name == user.Name && a.Password == user.Password).FirstOrDefault();
            return users != null ? true : false;
        }

        public bool IsExistsByName(string name)
        {
            var users = db.Users.Where(a => a.Name == name).FirstOrDefault();
            return users != null ? true : false;
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}