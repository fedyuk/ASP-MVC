using ASP.NET_Test.Models;
using ASP.NET_Test.Services;
using ASP.NET_Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_Test.Controllers
{
    public class TaskController : Controller
    {
        private ITaskService taskService = new TaskService();

        public ActionResult ShowTasks(int categoryId)
        {
            if(Session["LogedUserID"] != null)
            {
                return View(GetTasksWithTaskItemCounts(categoryId));
            }
            else
            {
                return RedirectToAction("LoginAccount", "Account");
            }
        }

        public List<TaskTaskItemCountViewModel> GetTasksWithTaskItemCounts(int categoryId)
        {
            var tasks = taskService.GetAll(categoryId).ToList();
            var taskTaskItemCount = new List<TaskTaskItemCountViewModel>();
            if (tasks != null)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    taskTaskItemCount.Add(new TaskTaskItemCountViewModel { Task = tasks[i], Count = taskService.GetCountTaskItemsByTaskId(tasks[i].TaskId), CategoryId = categoryId });
                }
                if (tasks.Count == 0)
                {
                    taskTaskItemCount.Add(new TaskTaskItemCountViewModel { Count = -1, CategoryId = categoryId });
                }
            }
            return taskTaskItemCount;
        }

        public ActionResult AddTask(int categoryId)
        {
            if (Session["LogedUserID"] != null)
            {
                var categoryTask = new CategoryTaskViewModel();
                categoryTask.CategoryId = categoryId;
                categoryTask.Task = new Task();
                return View(categoryTask);
            }
            else
            {
                return RedirectToAction("LoginAccount", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTask(CategoryTaskViewModel categoryTask)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    taskService.Add(categoryTask.CategoryId, categoryTask.Task);
                    taskService.Save();
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("ShowError", "Error");
                }
                return RedirectToAction("ShowTasks", new { categoryId = categoryTask.CategoryId});
            }
            else
            {
                return View(categoryTask);
            }
        }

        public ActionResult DeleteTask(int taskId, int categoryId)
        {
            if (Session["LogedUserID"] != null)
            {
                try
                {
                    taskService.Delete(taskId);
                    taskService.Save();
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("ShowTasks", new { categoryId = categoryId });
                }
                return RedirectToAction("ShowTasks", new { categoryId = categoryId });
            }
            else
            {
                return RedirectToAction("LoginAccount", "Account");
            }
        }

    }
}
