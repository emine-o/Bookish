namespace Bookish.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

    public ICollection<Item> Items { get; set; }

    public Book() {}
    public Book(int id, string title, string author)
    {
        Id = id;
        Title = title;
        Author = author;
    }
}
