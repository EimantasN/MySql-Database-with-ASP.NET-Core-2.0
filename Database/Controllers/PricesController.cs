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
    public class PricesController : Controller
    {
        public IActionResult Index()
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;

            List<Prices> List = new List<Prices>();
            foreach (var data in _context.GetList(new Prices { }))
            {
                List.Add((Prices)data);
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
            var items = _context.Details(id, new Prices { });
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
        public JsonResult GoodSave(UserCreate model)
        {
            //Console.WriteLine(model.user);
            //Console.WriteLine(model.city);
            //Console.WriteLine(String.Join(" ", model.FavoriteBands));
            return Json(new { result = "saved the good way" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,Price,Created,fk_Item_Prices")] Prices prices)
        {
            if (ModelState.IsValid)
            {
                Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                _context.Add(prices);
                return RedirectToAction("Index", "Prices");
            }
            return View(prices);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new Prices { });
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,Price,Created,fk_Item_Prices")] Prices prices)
        {
            if (id != prices.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                    var temp = _context.GetPrices().First(x => x.id == id);
                    prices.fk_Item_Prices = temp.id;
                    _context.Add(prices);
                }
                catch
                {
                    if (!PricesExists(prices.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Prices");
            }
            return View(prices);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new Prices { });
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
            _context.Delete(id, new Prices { });
            return RedirectToAction("Index", "Prices");
        }

        private bool PricesExists(int id)
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
