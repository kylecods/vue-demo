using Application.Services;
using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using vue_api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.AddAuthentication();
builder.Services.AddAuthorizationBuilder().AddCurrentUserHandler();
builder.Services.AddAuthorization(options => options.AddPolicy("RequireCurrentUser", policy =>
{
    policy.RequireCurrentUser();
}));

//register db context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddDbContext<UserIdentityDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
);

//Identity configuration
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<UserIdentityDbContext>();


//register respositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

//register services
builder.Services.AddScoped<IBookShelfService, BookShelfService>();

builder.Services.AddCurrentUser();

builder.Services.AddCors(options =>
{
    options.AddPolicy("VueAppPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseCors("VueAppPolicy");

app.MapControllers();

app.Run();
