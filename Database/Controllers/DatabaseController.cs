using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Database.Models;
using MySql.Data.MySqlClient;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Database.Controllers
{

    public class DatabaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region Citys
        public IActionResult CityList()
        {
            Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            List<Citys> CityList = new List<Citys>();
            foreach (var City in context.GetList(new Citys { }))
            {
                CityList.Add((Citys)City);
            }

            //context.Add(new Citys { Name = "Kaunas"});

            //context.Edit(1, new Citys { Name = "Update pavyko" });
            var model = new CityList
            {
                citys = CityList
            };
            return View(model);
        }

        public IActionResult CityAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CityAdd(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                context.Add(new Citys { Name = Name });
            }
            return RedirectToAction(nameof(CityList));
        }

        //// GET: Citys/Details/5
        //public IActionResult Details(int? id)
        //{
        //    Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var citys = null;
        //    if (citys == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(citys);
        //}

        //// GET: Citys/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Citys/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name")] Citys citys)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(citys);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(citys);
        //}

        //// GET: Citys/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var citys = await _context.Citys.SingleOrDefaultAsync(m => m.Id == id);
        //    if (citys == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(citys);
        //}

        //// POST: Citys/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Citys citys)
        //{
        //    if (id != citys.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(citys);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CitysExists(citys.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(citys);
        //}

        //// GET: Citys/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var citys = await _context.Citys
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (citys == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(citys);
        //}

        //// POST: Citys/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var citys = await _context.Citys.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.Citys.Remove(citys);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CitysExists(int id)
        //{
        //    return _context.Citys.Any(e => e.Id == id);
        //}
        #endregion



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
