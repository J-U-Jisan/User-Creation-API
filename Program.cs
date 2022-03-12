using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UserDb>(opt => opt.UseInMemoryDatabase("UserList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();


app.MapGet("/users", async (UserDb db) =>
    await db.Users.ToListAsync());

app.MapPost("/add-user", async (User user, UserDb db) =>
{
    db.Users.Add(user);
    await db.SaveChangesAsync();

});


app.Run();

[Keyless]
class User
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string gender { get; set; }
    public string dateOfBirth { get; set; }
    public string? city { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
}

class UserDb : DbContext
{
    public UserDb(DbContextOptions<UserDb> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
}