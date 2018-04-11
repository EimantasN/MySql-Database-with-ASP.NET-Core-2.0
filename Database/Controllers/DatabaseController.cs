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
        //private readonly Context _context;

        //public DatabaseController(HttpContext context)
        //{
        //    _context = context.RequestServices.GetService(typeof(Context)) as Context;
        //}


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CityList()
        {
            Context context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;

            var model = new CityList
            {
                citys = context.GetCitys()
            };
            return View(model);
        }

        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
