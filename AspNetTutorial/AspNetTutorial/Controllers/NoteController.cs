using AspNetTutorial.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;

namespace AspNetTutorial.Controllers
{
    public class NoteController : Controller
    {
        private INoteService noteService = new NoteService();

        private IUserService userService = new UserService();

        private UserNoteContext db = new UserNoteContext();

        private int currentNoteId = 0;
        //
        // GET: /Note/
        public ActionResult ShowNotes()
        {
            try
            {
                if (Session["LogedUserID"] != null)
                {
                    int currentId = Int32.Parse(Session["LogedUserID"].ToString());
                    var notes = noteService.GetByUserId(currentId);
                    return View(notes);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
            catch (SqlException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
        }

        public ActionResult CreateNote()
        {
            try
            {
                if (Session["LogedUserID"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNote(Note n)
        {
            try
            {
                if (String.IsNullOrEmpty(Request.Form["Content"].ToString()))
                {
                    User user = userService.GetById(Int32.Parse(Session["LogedUserID"].ToString()));
                    noteService.Add(user, new Note { Content = "Empty", CreateDate = DateTime.Now });
                }
                else
                {
                    User user = userService.GetById(Int32.Parse(Session["LogedUserID"].ToString()));
                    noteService.Add(user, new Note { Content = Request.Form["Content"], CreateDate = DateTime.Now });
                }
                noteService.Save();
                return RedirectToAction("ShowNotes", "Note");
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
            catch (SqlException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
        }

        public ActionResult DeleteNote(int id)
        {
            try
            {
                if (Session["LogedUserID"] != null)
                {
                    noteService.Delete(id);
                    noteService.Save();
                    return RedirectToAction("ShowNotes");
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
            catch (SqlException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
        }

        public ActionResult EditNote(int id)
        {
            try
            {
                if (Session["LogedUserID"] != null)
                {
                    var editNote = noteService.GetById(id);
                    currentNoteId = id;
                    return View(editNote);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
            catch (SqlException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNote(int id, FormCollection collection)
        {
            try
            {
                var note = noteService.GetById(id);
                note.Content = Request.Form["Content"].ToString();
                noteService.Update(note);
                noteService.Save();
                return RedirectToAction("ShowNotes");
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
            catch (SqlException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
        }

        public ActionResult SearchNote()
        {
            try
            {
                int currentUserId = Int32.Parse(Session["LogedUserID"].ToString());
                if (Session["LogedUserID"] != null)
                {
                    var note = noteService.GetByUserId(currentUserId);
                    return View(note);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
            catch (SqlException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchNote(FormCollection collection)
        {
            try
            {
                int currentUserId = Int32.Parse(Session["LogedUserID"].ToString());
                ViewData["SearchString"] = Request.Form["Content"].ToString();
                String searchString = Request.Form["Content"].ToString();
                var note = noteService.FindByContent(searchString, currentUserId);
                return View(note);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
            catch (SqlException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
        }
    }
}
