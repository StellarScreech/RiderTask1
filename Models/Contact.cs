using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Contact
{
    [Key]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("lastname")]
    public string LastName { get; set; }

    [Column("phonenumber")]
    public string PhoneNumber { get; set; }
}