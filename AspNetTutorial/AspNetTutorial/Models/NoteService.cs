using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AspNetTutorial.Models
{
    public class NoteService : INoteService
    {
        private UserNoteContext db;

        public NoteService()
        {
            db = new UserNoteContext();
        }

        public Note GetById(int id)
        {
            return db.Notes.Find(id);
        }

        public void Add(User user, Note note)
        {
            User findUser = db.Users.Find(user.UserId);
            findUser.Notes.Add(note);
        }

        public void Update(Note note)
        {
            db.Entry(note).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Note note = db.Notes.Find(id);
            db.Notes.Remove(note);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Note> SearchByContent(String content, int userId)
        {
            return db.Notes.Where(a => a.Content.Contains(content) && a.UserRefId == userId).ToList();
        }

        public IEnumerable<Note> GetByUserId(int userId)
        {
            return db.Notes.Where(a => a.UserRefId.Equals(userId)).ToList();
        }

        public IEnumerable<Note> FindByContent(String content, int userId)
        {
            return db.Notes.Where(a => a.Content.Contains(content) && a.UserRefId == userId).ToList();
        }
    }
}