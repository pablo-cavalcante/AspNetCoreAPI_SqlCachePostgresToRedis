using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

#pragma warning disable
namespace WebApplication7.Core.Core.Context
{
    public class PgsqlContext : DbContext
    {
        public PgsqlContext(DbContextOptions<PgsqlContext> options) : base(options) { }
    
        public DbSet<AgendaEntity> Agenda { get; set; }
    }
}
