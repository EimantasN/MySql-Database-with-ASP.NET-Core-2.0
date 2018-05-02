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
    public class CitysController : Controller
    {
        private readonly DatabaseContext _context;

        public CitysController(DatabaseContext context)
        {
            _context = context;
        }

        
    }
}
