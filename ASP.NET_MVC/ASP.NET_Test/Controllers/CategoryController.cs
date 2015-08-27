using ASP.NET_Test.Models;
using ASP.NET_Test.Services;
using ASP.NET_Test.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_Test.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService categoryService = new CategoryService();

        public ActionResult ShowCategories()
        {
            if (Session["LogedUserID"] != null)
            {
                var currentAccountId = Int32.Parse(Session["LogedUserID"].ToString());
                if (currentAccountId > 0)
                {
                    return View(GetCategoriesWithTaskCounts());
                }
                else
                {
                    return RedirectToAction("ShowError", "Error");
                }
            }
            else
            {
                return RedirectToAction("LoginAccount", "Account");
            }
        }

        public List<CategoryTaskCountViewModel> GetCategoriesWithTaskCounts()
        {
            var currentAccountId = Int32.Parse(Session["LogedUserID"].ToString());
            var categories = categoryService.GetAllByAccountId(currentAccountId).ToList();
            var categoryTaskCount = new List<CategoryTaskCountViewModel>();
            if (categories != null)
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    categoryTaskCount.Add(new CategoryTaskCountViewModel { Category = categories[i], Count = categoryService.GetCountTaskByCategoryId(categories[i].CategoryId) });
                }
            }
            return categoryTaskCount;
        }

        public ActionResult AddCategory()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginAccount", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory([Bind(Include = "CategoryId,CategoryName")]Category category)
        {
            var currentAccountId = Int32.Parse(Session["LogedUserID"].ToString());
            if (ModelState.IsValid)
            {
                try
                {
                    categoryService.Add(currentAccountId, category);
                    categoryService.Save();
                }
                catch (DbUpdateException)
                {
                    ViewData["UniqueError"] = "That category is already exists";
                    return View(category);
                }
                return RedirectToAction("ShowCategories", "Category");
            }
            else
            {
                return View(category);
            }
        }

        public ActionResult EditCategory(int categoryId)
        {
            if (Session["LogedUserID"] != null)
            {
                var category = categoryService.GetById(categoryId);
                if (category != null)
                {
                    return View(category);
                }
                else
                {
                    return RedirectToAction("ShowError", "Error");
                }
            }
            else
            {
                return RedirectToAction("LoginAccount", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory([Bind(Include = "CategoryId,CategoryName")]Category category)
        {
            if (ModelState.IsValid)
            {
                var existedCategory = categoryService.GetById(category.CategoryId);
                if (existedCategory != null)
                {
                    existedCategory.CategoryName = category.CategoryName;
                    try
                    {
                        categoryService.Edit(existedCategory);
                        categoryService.Save();
                    }
                    catch (DbUpdateException)
                    {
                        return RedirectToAction("ShowError", "Error");
                    }
                    return RedirectToAction("ShowCategories", "Category");
                }
                else
                {
                    return RedirectToAction("ShowError", "Error");
                }
            }
            else
            {
                return View(category);
            }
        }

        public ActionResult DeleteCategory(int categoryId)
        {
            if (Session["LogedUserID"] != null)
            {
                try
                {
                    categoryService.Delete(categoryId);
                    categoryService.Save();
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("ShowCategories", "Category");
                }
                return RedirectToAction("ShowCategories", "Category");
            }
            else
            {
                return RedirectToAction("LoginAccount", "Account");
            }
        }

    }
}
