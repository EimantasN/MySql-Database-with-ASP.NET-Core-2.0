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
    public class Users1Controller : Controller
    {
        private readonly DatabaseContext _context;

        public Users1Controller(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Users1
        
    }
}
