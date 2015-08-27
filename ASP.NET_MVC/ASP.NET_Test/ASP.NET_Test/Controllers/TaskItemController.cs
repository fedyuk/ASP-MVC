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
    public class TaskItemController : Controller
    {
        private ITaskItemService taskItemService = new TaskItemService();

        public ActionResult ShowTaskItems(int taskId)
        {
            if (Session["LogedUserID"] != null)
            {
                var taskItems = taskItemService.GetAll(taskId);
                if (taskItems == null)
                {
                    return RedirectToAction("ShowError", "Error");
                }
                var taskTaskItems = new TaskTaskItemsViewModel();
                taskTaskItems.TaskId = taskId;
                taskTaskItems.TaskItems = taskItems;
                return View(taskTaskItems);
            }
            else
            {
                return RedirectToAction("LoginAccount", "Account");
            }
        }

        public ActionResult AddTaskItem(int taskId)
        {
            if (Session["LogedUserID"] != null)
            {
                var taskTaskItem = new TaskTaskItemViewModel();
                taskTaskItem.TaskId = taskId;
                taskTaskItem.Task = new TaskItem();
                return View(taskTaskItem);
            }
            else
            {
                return RedirectToAction("LoginAccount", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTaskItem(TaskTaskItemViewModel taskTaskItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    taskItemService.Add(taskTaskItem.TaskId, taskTaskItem.Task);
                    taskItemService.Save();
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("ShowError", "Error");
                }
                return RedirectToAction("ShowTaskItems", new { taskId = taskTaskItem.TaskId });
            }
            else
            {
                return View(taskTaskItem);
            }
        }

        public ActionResult DeleteTaskItem(int taskItemId, int taskId)
        {
            if (Session["LogedUserID"] != null)
            {
                try
                {
                    taskItemService.Delete(taskItemId);
                    taskItemService.Save();
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("ShowTaskItems", new { taskId = taskId });
                }
                return RedirectToAction("ShowTaskItems", new { taskId = taskId });
            }
            else
            {
                return RedirectToAction("LoginAccount", "Account");
            }
        }

        public ActionResult CheckTaskItem(int taskItemId, int taskId)
        {
            if (Session["LogedUserID"] != null)
            {
                var taskItem = taskItemService.GetById(taskItemId);
                if (taskItem == null)
                {
                    return RedirectToAction("ShowTaskItems", new { taskId = taskId });
                }
                try
                {
                    taskItemService.ChangeStatus(taskItemId);
                    taskItemService.Save();
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("ShowError", "Error");
                }
                return RedirectToAction("ShowTaskItems", new { taskId = taskId });
            }
            else
            {
                return RedirectToAction("LoginAccount", "Account");
            }
        }

    }
}
