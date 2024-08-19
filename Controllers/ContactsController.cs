using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class ContactsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContactsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddContact(string fullName, string phoneNumber, int id)
    {
        if (string.IsNullOrEmpty(fullName))
        {
            return BadRequest("Полное имя не может быть пустым.");
        }

        var nameParts = fullName.Split(' ');
        if (nameParts.Length < 2)
        {
            return BadRequest("Полное имя должно содержать имя и фамилию.");
        }

        var contact = new Contact
        {
            Name = nameParts[0],
            LastName = nameParts[1],
            PhoneNumber = phoneNumber
        };
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }
}