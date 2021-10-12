using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySchedulerWeb.Data;
using MySchedulerWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MySchedulerWeb.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly MySchedulerWebContext _context;

        public UsersController(MySchedulerWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            return Content($"Your role : {role}");
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> List()
        {

            return View(await _context.Users.Include(u => u.DayEvents).ThenInclude(e => e.Users).ToListAsync());
        }


    }
}
