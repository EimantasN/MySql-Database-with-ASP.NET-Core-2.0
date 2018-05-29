using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database;
using Database.Models;
using System.IO;
using System.Text;
using Newtonsoft.Json;

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
        public IActionResult AA1()
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            List<AA1> List = new List<AA1>();
            foreach (var data in _context.GetListCategorys(new AA1 { }))
            {
                List.Add((AA1)data);
            }

            return View(List);
        }

        public IActionResult G4()
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            List<G4> List = new List<G4>();
            G4 prev = new G4();
            foreach (var data in _context.G4(new G4 { }))
            {
                if (List.Count() != 0)
                {
                    G4 current = (G4)data;
                    if (prev.CategoryName == current.CategoryName)
                    {
                        G4 model = current;
                        model.CategoryName = "";
                        model.CategoryCreated = "";

                        List.Add(model);
                    }
                    else
                    {
                        prev = current;
                        List.Add(current);
                    }
                }
                else
                {
                    List.Add((G4)data);
                    prev = (G4)data;
                }
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

        [HttpPost]
        public JsonResult EditImage()
        {
            try
            {
                int id = 0;
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    string a = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<items>(a);
                    items data = (items)result;
                    Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                    _context.Edit(data.Id, data);
                    try
                    {
                        id = data.Id;
                    }
                    catch
                    {
                        return Json("klaida");
                    }
                }
                return Json(id);
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public JsonResult GoodSave()
        {
            try
            {
                int id = 0;
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    string a = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<items>(a);
                    items item = (items)result;
                    Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                    _context.Add(item);
                    try
                    {
                        id = _context.GetItems().Last(x => x.Name == item.Name).Id;
                    }
                    catch
                    {
                        return Json("klaida");
                    }
                }
                return Json(id);
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public JsonResult Remove()
        {
            try
            {
                int id = 0;
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    string a = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<string>(a);
                    Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                    try
                    {
                        id = Int32.Parse(result);
                        _context.Delete(id, new items { });
                    }
                    catch
                    {
                        return Json("klaida");
                    }
                }
                return Json(id);
            }
            catch
            {
                return null;
            }
        }

        public IActionResult Create()
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            foreach (items data in _context.GetItems().Where(x => x.fk_Category_Items == 0))
            {

                _context.Delete(data.Id, new items { });
            }
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
                try
                {
                    int id = _context.GetCategorys().Where(x => x.id == _context.GetCategorys().Max(y => y.id)).First().id;
                    foreach (items data in _context.GetItems().Where(x => x.fk_Category_Items == 0))
                    {
                        data.fk_Category_Items = id;
                        _context.Edit(data.Id, data);
                    }
                }
                catch
                {
                    return View(categorys);
                }

                return RedirectToAction("Index", "Categorys");
            }
            return View(categorys);
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new Categorys { });

            try
            {
                // int id = _context.GetCategorys().Where(x => x.id == _context.GetCategorys().Max(y => y.id)).First().id;
                foreach (items data in _context.GetItems().Where(x => x.fk_Category_Items == 0))
                {
                    data.fk_Category_Items = id;
                    _context.Edit(data.Id, data);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Categorys");
            }

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
