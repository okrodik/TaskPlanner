using Microsoft.EntityFrameworkCore;
using TaskPlanner.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlite("Data Source=taskplanner.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Tasks.Any())
    {
        dbContext.Tasks.AddRange(
            new TaskPlanner.Models.TaskItem
            {
                Title = "Task 1",
                Description = "First task",
                CreatedDate = DateTime.Now.AddDays(-2),
                IsCompleted = true,
                CompletedDate = DateTime.Now.AddDays(-1)
            },
            new TaskPlanner.Models.TaskItem
            {
                Title = "Task 2",
                Description = "Second task",
                CreatedDate = DateTime.Now.AddDays(-1),
                IsCompleted = false
            }
        );
        dbContext.SaveChanges();
    }
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");
app.Run();