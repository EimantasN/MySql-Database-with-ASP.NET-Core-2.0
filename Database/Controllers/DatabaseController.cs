using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Database.Models;
using MySql.Data.MySqlClient;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Database.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            List<Users> List = new List<Users>();
            foreach (var data in _context.GetList(new Users { }))
            {
                List.Add((Users)data);
            }
            return View(List);
        }

        // GET: Users1/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var users = _context.Details(id, new Users { });
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,Name,LastName,UserName,Password,Email,Gender,Income,Phone_nr,Mobile_nr,IsComany,Company_name,Age,Street_adress,Show_email,Show_phone_nr,Show_mobile_nr,Show_location,Show_the_exact_address,Allow_send_me_messages,How_often_inform,Which_time_inform,Send_fovorite_update,Send_history_update,Send_comments_update,Send_conversation_update,Send_intrest_new_items_updates,Send_notification_about_new_item_in_your_city,Send_notification_about_intrest_in_your_item,Send_notification_about_comment_on_your_item,Adverts_limit,Images_size_limit,Images_count_limit,Use_comment_limit,User_message_limit,User_favorite_items_limit,HasCompletedPersonalInfo,Is_vip,Is_banned,Duration_of_ban,Reason_of_ban,Education,fk_City_Users")] Users users)
        {
            if (ModelState.IsValid)
            {
                Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                _context.Add(users);
                return RedirectToAction("Index", "User");
            }
            return View(users);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var users = _context.Details(id, new Users { });
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,Name,LastName,UserName,Password,Email,Gender,Income,Phone_nr,Mobile_nr,IsComany,Company_name,Age,Street_adress,Show_email,Show_phone_nr,Show_mobile_nr,Show_location,Show_the_exact_address,Allow_send_me_messages,How_often_inform,Which_time_inform,Send_fovorite_update,Send_history_update,Send_comments_update,Send_conversation_update,Send_intrest_new_items_updates,Send_notification_about_new_item_in_your_city,Send_notification_about_intrest_in_your_item,Send_notification_about_comment_on_your_item,Adverts_limit,Images_size_limit,Images_count_limit,Use_comment_limit,User_message_limit,User_favorite_items_limit,HasCompletedPersonalInfo,Is_vip,Is_banned,Duration_of_ban,Reason_of_ban,Education,fk_City_Users")] Users users)
        {
            if (id != users.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                    _context.Edit(id, users);
                }
                catch
                {
                    if (!UsersExists(users.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "User");
            }
            return View(users);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var users = _context.Details(id, new Users { });
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var users = _context.Details(id, new Users { });
            _context.Delete(id, new Users { });
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var data =_context.Details(id, new Users { });
            if (data != null)
                return true;
            return false;
        }
    }

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



        #endregion


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
