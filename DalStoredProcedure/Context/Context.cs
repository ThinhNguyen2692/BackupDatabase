using ModelProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Bus_backUpData.Data
{
    public class Context : DbContext
    {
        private string _con = Setting.ConnectionStrings;
        public Context() { }
        public Context(string ConnectionStrings)
        {
            _con = ConnectionStrings;
        }
        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_con);
        }    
    }
}
