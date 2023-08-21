using Microsoft.EntityFrameworkCore;

namespace EmptyStack.Data;

public class EmptyStackDb : DbContext
{
    public EmptyStackDb(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User>? users { get; set; }
    public DbSet<Question>? questions { get; set; }
    public DbSet<Answer>? answers { get; set; }
}