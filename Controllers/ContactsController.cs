using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class ContactsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ContactsController> _logger;

    public ContactsController(ApplicationDbContext context, ILogger<ContactsController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddContact(string firstName, string lastName, string middleName, string phoneNumber)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return BadRequest("Имя и фамилия не могут быть пустыми.");
        }

        string fullName = $"{firstName} {lastName} {middleName}";
        _logger.LogInformation("Received fullName: {FullName}", fullName);

        var contact = new Contact
        {
            Name = firstName,
            LastName = lastName,
            MiddleName = middleName,
            PhoneNumber = phoneNumber
        };
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }
}