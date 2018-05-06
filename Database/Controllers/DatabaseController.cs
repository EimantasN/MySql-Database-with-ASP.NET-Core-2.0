using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Database.Models;
using MySql.Data.MySqlClient;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Linq;

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

        // GET: Citys/Details/5
        public IActionResult Details(int? id)
        {
            Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            if (id == null)
            {
                return NotFound();
            }

            var citys = context.Details(id, new Citys { });
            if (citys == null)
            {
                return NotFound();
            }

            return View(citys);
        }

        // GET: Citys/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Citys citys)
        {
            if (ModelState.IsValid)
            {
                Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                context.Add(citys);
                return RedirectToAction(nameof(CityList));
            }
            return View(citys);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var citys = context.Details(id, new Citys { });
            if (citys == null)
            {
                return NotFound();
            }
            return View(citys);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("Id,Name")] Citys citys)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                    context.Edit(id, citys);
                }
                catch
                {
                        return NotFound();
                }
                return RedirectToAction(nameof(CityList));
            }
            return View(citys);
        }


        public IActionResult Delete(int? id)
        {
            Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            if (id == null)
            {
                return NotFound();
            }

            var citys = context.Details(id, new Citys { });
            if (citys == null)
            {
                return NotFound();
            }

            return View(citys);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            context.Delete(id, new Citys { });
            return RedirectToAction(nameof(CityList));
        }

        private bool CitysExists(int id)
        {
            Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var OBJEKTAS = context.Details(id, new Citys { });
            if (OBJEKTAS != null)
            {
                return true;
            }
            else
                return false;
        }
        #endregion

        #region user

        public IActionResult UserList()
        {
            Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            List<Users> List = new List<Users>();
            foreach (var User in context.GetList(new Users { }))
            {
                List.Add((Users)User);
            }
            return View(List.ToAsyncEnumerable());
        }

        #endregion


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
