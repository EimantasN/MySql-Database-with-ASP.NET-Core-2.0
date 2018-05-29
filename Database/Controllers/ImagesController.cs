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
    //        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,ImageThumbnail,fk_Item_Images")] Images images)
    //        
    public class ImagesController : Controller
    {
        public IActionResult Index()
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            List<Images> List = new List<Images>();
            foreach (var data in _context.GetList(new Images { }))
            {
                List.Add((Images)data);
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
            var items = _context.Details(id, new Images { });
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
        public IActionResult Create([Bind("id,Image,Image_thumbnail,fk_Item_Images")] Images images)
        {
            if (ModelState.IsValid)
            {
                Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                _context.Add(images);
                return RedirectToAction("Index", "Images");
            }
            return View(images);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new Images { });
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,Image,Image_thumbnail,fk_Item_Images")] Images images)
        {
            if (id != images.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                    _context.Edit(id, images);
                }
                catch
                {
                    if (!ImagesExists(images.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Images");
            }
            return View(images);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new Images { });
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
            _context.Delete(id, new Images { });
            return RedirectToAction("Index", "Images");
        }

        private bool ImagesExists(int id)
        {
            try
            {
                Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                var data = _context.Details(id, new Images { });
                if (data != null)
                    return true;
                return false;
            }
            catch { return false; }
        }
    }
}
