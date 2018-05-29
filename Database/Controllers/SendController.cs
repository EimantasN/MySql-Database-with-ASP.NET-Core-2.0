using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Database.Controllers
{
    [Produces("application/json")]
    [Route("api/Send")]
    public class SendController : Controller
    {
        [HttpPost]
        public IActionResult GoodSave([FromBody] Images model)
        {
            string a = model.Image;
            Context _context = HttpContext.RequestServices.GetService(typeof(Context)) as Context;
            //_context.Add(model);
            //return RedirectToAction("Edit", "Items");
            return Json(model.Image);

        }
    }
}