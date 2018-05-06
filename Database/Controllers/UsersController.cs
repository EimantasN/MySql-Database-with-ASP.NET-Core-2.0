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
    public class UsersController : Controller
    {

        // GET: Users


        //// GET: Users/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var users = await _context.Users
        //        .SingleOrDefaultAsync(m => m.id == id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(users);
        //}

        //// GET: Users/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("id,Name,lastName,UserName,Password,Email,Gender,Income,PhoneNr,MobileNr,IsComany,Company_name,Age,Street_adress,Show_email,Show_phone_nr,Show_mobile_nr,Show_location,Show_the_exact_address,Allow_send_me_messages,How_often_inform,Which_time_inform,Send_fovorite_update,Send_history_update,Send_comments_update,Send_conversation_update,Send_intrest_new_items_updates,Send_notification_about_new_item_in_your_city,Send_notification_about_intrest_in_your_item,Send_notification_about_comment_on_your_item,Adverts_limit,Images_size_limit,Images_count_limit,Use_comment_limit,User_message_limit,User_favorite_items_limit,HasCompletedPersonalInfo,Is_vip,Is_banned,Duration_of_ban,Reason_of_ban,Education,fk_City_Users")] Users users)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(users);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(users);
        //}

        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var users = await _context.Users.SingleOrDefaultAsync(m => m.id == id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(users);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("id,Name,lastName,UserName,Password,Email,Gender,Income,PhoneNr,MobileNr,IsComany,Company_name,Age,Street_adress,Show_email,Show_phone_nr,Show_mobile_nr,Show_location,Show_the_exact_address,Allow_send_me_messages,How_often_inform,Which_time_inform,Send_fovorite_update,Send_history_update,Send_comments_update,Send_conversation_update,Send_intrest_new_items_updates,Send_notification_about_new_item_in_your_city,Send_notification_about_intrest_in_your_item,Send_notification_about_comment_on_your_item,Adverts_limit,Images_size_limit,Images_count_limit,Use_comment_limit,User_message_limit,User_favorite_items_limit,HasCompletedPersonalInfo,Is_vip,Is_banned,Duration_of_ban,Reason_of_ban,Education,fk_City_Users")] Users users)
        //{
        //    if (id != users.id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(users);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UsersExists(users.id))
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
        //    return View(users);
        //}

        //// GET: Users/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var users = await _context.Users
        //        .SingleOrDefaultAsync(m => m.id == id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(users);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var users = await _context.Users.SingleOrDefaultAsync(m => m.id == id);
        //    _context.Users.Remove(users);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UsersExists(int id)
        //{
        //    return _context.Users.Any(e => e.id == id);
        //}
    }
}
