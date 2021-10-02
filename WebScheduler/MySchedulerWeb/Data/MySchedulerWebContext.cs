using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySchedulerWeb.Models;

namespace MySchedulerWeb.Data
{
    public class MySchedulerWebContext : DbContext
    {
        public MySchedulerWebContext (DbContextOptions<MySchedulerWebContext> options)
            : base(options)
        {
        }

        public DbSet<MySchedulerWeb.Models.DayEvent> DayEvent { get; set; }
    }
}
