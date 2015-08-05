using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetTutorial.Models
{
    interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByUserNameAndPassword(String name, String password);
        User GetByUserName(String name);
        User Add(User user);
        void Update(User user);
        void Delete(int id);
        void Save();
        bool IsExists(User user);
        bool IsExistsByName(string name);
    }
}
