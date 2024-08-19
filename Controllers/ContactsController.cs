using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
        _logger.LogInformation("AddContact method called with parameters: {FirstName}, {LastName}, {MiddleName}, {PhoneNumber}", firstName, lastName, middleName, phoneNumber);

        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return BadRequest("Имя и фамилия не могут быть пустыми.");
        }

        string fullName = $"{firstName} {lastName} {middleName}";
        _logger.LogInformation("Received fullName: {FullName}", fullName);
        _logger.LogInformation("Received phoneNumber: {PhoneNumber}", phoneNumber);

        var contact = new Contact
        {  
            Name = firstName,
            LastName = lastName,
            MiddleName = middleName,
            PhoneNumber = phoneNumber,
        };
        _context.Contacts.Add(contact);
        _logger.LogInformation("Contact added to context: {Contact}", contact);

        await _context.SaveChangesAsync();
        _logger.LogInformation("Changes saved to database");

        return RedirectToAction("Index", "Home");
    }
}