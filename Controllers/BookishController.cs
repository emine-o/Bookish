using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Bookish.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bookish.Controllers;

public class BookishController : Controller
{
    private readonly BookishDbContext _context;

    public BookishController(BookishDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        List<Book> books = (await _context.Book.ToListAsync()).ToList();
        return View(books);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // GET: Bookish/Edit/<int: id>
    public async Task<IActionResult> EditBook(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var book = await _context.Book.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }
    
    // POST: Bookish/Edit/<int: id>
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditBook(int id, Book book, [Bind("Title,Author")] Book bookToUpdate)
    {
        if (id != book.Id)
        {
            return NotFound();
        }

        Console.WriteLine($"{book.Id}, {book.Title}, {book.Author}");
        if (ModelState.IsValid)
        {
            _context.Book.Update(bookToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        Console.WriteLine("If statement failed.");
        return View(book);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
