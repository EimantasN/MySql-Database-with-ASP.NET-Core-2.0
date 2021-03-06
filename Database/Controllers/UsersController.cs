﻿using System;
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
    public class UsersController : Controller
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

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            var items = _context.Details(id, new Users { });
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
        public IActionResult Create([Bind("Name,LastName,UserName,Password,Email,Gender,Income,Phone_nr,Mobile_nr,IsComany,Company_name,Age,Show_email,Show_phone_nr,Show_mobile_nr,Show_location,Show_the_exact_address,Allow_send_me_messages,How_often_inform,Which_time_inform,Send_fovorite_update,Send_history_update,Send_comments_update,Send_conversation_update,Send_intrest_new_items_updates,Send_notification_about_new_item_in_your_city,Send_notification_about_intrest_in_your_item,Send_notification_about_comment_on_your_item,Street_adress,Adverts_limit,Images_size_limit,Images_count_limit,Use_comment_limit,User_message_limit,User_favorite_items_limit,HasCompletedPersonalInfo,Is_vip,Is_banned,Duration_of_ban,Reason_of_ban,Education,id, fk_City_Users")] Users users)
        {
            if (ModelState.IsValid)
            {
                Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
                _context.Add(users);
                return RedirectToAction("Index", "Users");
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
            var items = _context.Details(id, new Users { });
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,LastName,UserName,Password,Email,Gender,Income,Phone_nr,Mobile_nr,IsComany,Company_name,Age,Show_email,Show_phone_nr,Show_mobile_nr,Show_location,Show_the_exact_address,Allow_send_me_messages,How_often_inform,Which_time_inform,Send_fovorite_update,Send_history_update,Send_comments_update,Send_conversation_update,Send_intrest_new_items_updates,Send_notification_about_new_item_in_your_city,Send_notification_about_intrest_in_your_item,Send_notification_about_comment_on_your_item,Street_adress,Adverts_limit,Images_size_limit,Images_count_limit,Use_comment_limit,User_message_limit,User_favorite_items_limit,HasCompletedPersonalInfo,Is_vip,Is_banned,Duration_of_ban,Reason_of_ban,Education,id,fk_City_Users")] Users users)
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
                return RedirectToAction("Index", "Users");
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
            var items = _context.Details(id, new Users { });
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
            _context.Delete(id, new Users { });
            return RedirectToAction("Index", "Users");
        }

        private bool UsersExists(int id)
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
