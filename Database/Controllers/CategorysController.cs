using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database;
using Database.Models;

namespace Database.Controllers
{
    //([Bind("id,Name,Created,Updated")] Categorys categorys)
    public class CategorysController : Controller
    {
        public IActionResult Index()
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            List<Categorys> List = new List<Categorys>();
            foreach (var data in _context.GetList(new Categorys { }))
            {
                List.Add((Categorys)data);
            }

            return View(List);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new Categorys { });
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,Name,Created,Updated")] Categorys categorys)
        {
            if (ModelState.IsValid)
            {
                Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                _context.Add(categorys);
                return RedirectToAction("Index", "Categorys");
            }
            return View(categorys);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new Categorys { });
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("id,Name,Created,Updated")] Categorys categorys)
        {
            if (id != categorys.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                    _context.Edit(id, categorys);
                }
                catch
                {
                    if (!CategoryExists(categorys.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Categorys");
            }
            return View(categorys);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new Categorys { });
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            _context.Delete(id, new Categorys { });
            return RedirectToAction("Index", "Categorys");
        }

        private bool CategoryExists(int id)
        {
            try
            {
                Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                var data = _context.Details(id, new Categorys { });
                if (data != null)
                    return true;
                return false;
            }
            catch { return false; }
        }
    }
}
