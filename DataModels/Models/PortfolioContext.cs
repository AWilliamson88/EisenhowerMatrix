using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Models
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options)
        {
        }

        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

    }
}
