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

    // GET: Bookish/CreateBook/
    public IActionResult CreateBook()
    {
        return View();
    }

    // POST: Bookish/CreateBook/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateBook([Bind("Title,Author")] Book book)

    {
        if (!ModelState.IsValid)
        {
            return View(book);
        }

        // var books = from aBook in _context.Book
        //     select aBook;
        
        // Book? duplicateBook = books.Where(aBook => aBook.Title == book.Title && aBook.Author == book.Author).FirstOrDefault();
        // if (duplicateBook == null)
        {
            _context.Book.Add(new Book(book.Title, book.Author));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // ViewData["ErrorMessage"] = "Sorry, you can't add this book because it already exists in the catalogue.";
        // return View(book);
    }

    // GET: Bookish/EditBook/<int: id>
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
    
    // POST: Bookish/EditBook/<int: id>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditBook(int id, [Bind("Id,Title,Author")] Book book)
    {
        if (id != book.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var books = from aBook in _context.Book
                select aBook;
        
            Book? duplicateBook = books.Where(aBook => aBook.Title == book.Title && aBook.Author == book.Author).FirstOrDefault();
            if (duplicateBook == null)
            {
                _context.Book.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        ViewData["ErrorMessage"] = "Sorry, you can't add this book because it already exists in the catalogue.";
        return View(book);
    }
    // public async Task<IActionResult> DeleteBook(int? id)
    // {
    //     if (id == null)
    //     {
    //         return NotFound();
    //     }

    //     var book = await _context.Book.FindAsync(id);
    //     if (book == null)
    //     {
    //         return NotFound();
    //     }
    // }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
