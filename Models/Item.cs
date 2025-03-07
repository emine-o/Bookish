namespace Bookish.Models;

public class Item
{
    public int Id { get; set; }
    public Book Book { get; set; }
    public User? User { get; set; }
}