using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace Database.Controllers
{
    [Produces("application/json")]
    public class itemsController : Controller
    {
        // GET: Items
        public IActionResult Index()
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            List<items> List = new List<items>();
            foreach (var data in _context.GetList(new items { }))
            {
                List.Add((items)data);
            }

            return View(List);
        }

        // GET: Items/Details/id
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new items { });
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            foreach (Images data in _context.GetImages().Where(x => x.fk_Item_Images == 0))
            {

                _context.Delete(data.id, new Images { });
            }
            return View();
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
                    var result = JsonConvert.DeserializeObject<Images>(a);
                    Images image = (Images)result;
                    Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                    if (string.IsNullOrEmpty(image.Image) && string.IsNullOrEmpty(image.Image_thumbnail))
                    {
                        return Json("klaida");
                    }
                    _context.Edit(image.id, image);
                    try
                    {
                        id = image.id;
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
                    var result = JsonConvert.DeserializeObject<Images>(a);
                    Images image = (Images)result;
                    Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                    if (string.IsNullOrEmpty(image.Image) && string.IsNullOrEmpty(image.Image_thumbnail))
                    {
                        return Json("klaida");
                    }
                    _context.Add(image);
                    try
                    {
                        id = _context.GetImages().Last(x => x.Image == image.Image).id;
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
                        _context.Delete(id, new Images { });
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

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Quantity,Description,Report_likes,ReportAboutComment,Report_about_offer,Visited_times,Received_offers,Created,Updated,fk_Category_Items,fk_User_Items, Status,Image,Image_thumbnail,fk_Item_Images")] items item)
        {
            if (ModelState.IsValid)
            {
                Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                _context.Add(item);
                try
                {
                    int id = _context.GetItems().Where(x => x.Id == _context.GetItems().Max(y => y.Id)).First().Id;
                    foreach (Images data in _context.GetImages().Where(x => x.fk_Item_Images == 0))
                    {
                        //data.fk_Item_Images = item.Id;
                        Images model = new Images
                        {
                            Image = data.Image,
                            Image_thumbnail = data.Image_thumbnail,
                            fk_Item_Images = id,
                            id = data.id
                        };

                        _context.Edit(model.id, model);
                    }
                }
                catch {

                }

                //_context.Add(skelbimas);
                return RedirectToAction("Index", "Items");
            }
            return View(item);
        }

        // GET: Items/Edit/id
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new items { });

            foreach (Images data in _context.GetImages().Where(x => x.fk_Item_Images == 0))
            {

                _context.Delete(data.id, new Images { });
            }

            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        // POST: Items/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Quantity,Description,Report_likes,ReportAboutComment,Report_about_offer,Visited_times,Received_offers,Created,Updated,fk_Category_Items,fk_User_Items, Status")] items items)
        {
            if (id != items.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                    _context.Edit(id, items);

                    foreach (Images data in _context.GetImages().Where(x => x.fk_Item_Images == 0))
                    {
                        //data.fk_Item_Images = item.Id;
                        Images model = new Images
                        {
                            Image = data.Image,
                            Image_thumbnail = data.Image_thumbnail,
                            fk_Item_Images = id,
                            id = data.id
                        };

                        _context.Edit(model.id, model);
                    }
                }
                catch
                {
                    if (!ItemsExists(items.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(items);
        }

        // GET: Items/Delete/id
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new items { });
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: Items/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            _context.Delete(id, new items { });
            foreach (Images data in _context.GetImages().Where(x => x.fk_Item_Images == id))
            {

                _context.Delete(data.id, new Images { });
            }
            return RedirectToAction("Index", "Prices");
        }

        private bool ItemsExists(int id)
        {
            try
            {
                Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                var data = _context.Details(id, new items { });
                if (data != null)
                    return true;
                return false;
            }
            catch { return false; }
        }
    }
}
