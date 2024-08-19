using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

static void PrintContacts(ApplicationDbContext context)
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    var contacts = context.Contacts.ToList();
    foreach (var contact in contacts)
    {
        Console.WriteLine($"ID: {contact.Id}, Name: {contact.Name}, LastName: {contact.LastName}, MiddleName: {contact.MiddleName}, PhoneNumber: {contact.PhoneNumber}");
    }
}

// Seed the database and print contacts
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    if (!context.Contacts.Any())
    {
        context.Contacts.AddRange(
            new Contact
            {
                Name = "Иван",
                LastName = "Иванов",
                MiddleName = "Иванович",
                PhoneNumber = "1234567890"
            },
            new Contact
            {
                Name = "Петр",
                LastName = "Петров",
                MiddleName = "Петрович",
                PhoneNumber = "0987654321"
            }
        );
        context.SaveChanges();
    }
    PrintContacts(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();