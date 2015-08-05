using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetTutorial.Models
{
    interface INoteService
    {
        Note GetById(int id);
        IEnumerable<Note> GetByUserId(int userId);
        void Update(Note note);
        void Delete(int id);
        void Save();
        IEnumerable<Note> SearchByContent(String content, int userId);
        void Add(User user, Note note);
        IEnumerable<Note> FindByContent(String content, int userId);
    }
}
