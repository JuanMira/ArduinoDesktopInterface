using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialUI.MVVM.Model
{
    internal class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            
        }
        public virtual DbSet<DataModel> Data { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={Environment.CurrentDirectory}/database.db");
    }
}
