using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KsiegarniaOnline.DAL;
using KsiegarniaOnline.Models;

namespace KsiegarniaOnline.Controllers
{
    public class BooksController : Controller
    {
        private readonly KsiegarniaOnlineContext _context;

        public BooksController(KsiegarniaOnlineContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var ksiegarniaOnlineContext = _context.Books.Include(b => b.Author).Include(b => b.BookCategory).Include(b => b.Category).Include(b => b.PublishHouse);
            return View(await ksiegarniaOnlineContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookCategory)
                .Include(b => b.Category)
                .Include(b => b.PublishHouse)
                .FirstOrDefaultAsync(m => m.IdBook == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Authors, "IdAuthor", "FirstName");
            ViewData["BookCategoryID"] = new SelectList(_context.BookCategories, "CategoryID", "CategoryBook");
            ViewData["CategoryID"] = new SelectList(_context.Categories, "IdCategory", "KindOfBook");
            ViewData["PublishHouseID"] = new SelectList(_context.PublishHouses, "IdPublishHouse", "PublishHouseName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBook,Title,Price,Quantity,NumberOfPages,Description,Binding,IBSN,BookCategoryID,AuthorID,PublishHouseID,CategoryID")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "IdAuthor", "FirstName", book.AuthorID);
            ViewData["BookCategoryID"] = new SelectList(_context.BookCategories, "CategoryID", "CategoryBook", book.BookCategoryID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "IdCategory", "KindOfBook", book.CategoryID);
            ViewData["PublishHouseID"] = new SelectList(_context.PublishHouses, "IdPublishHouse", "PublishHouseName", book.PublishHouseID);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "IdAuthor", "FirstName", book.AuthorID);
            ViewData["BookCategoryID"] = new SelectList(_context.BookCategories, "CategoryID", "CategoryBook", book.BookCategoryID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "IdCategory", "KindOfBook", book.CategoryID);
            ViewData["PublishHouseID"] = new SelectList(_context.PublishHouses, "IdPublishHouse", "PublishHouseName", book.PublishHouseID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBook,Title,Price,Quantity,NumberOfPages,Description,Binding,IBSN,BookCategoryID,AuthorID,PublishHouseID,CategoryID")] Book book)
        {
            if (id != book.IdBook)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.IdBook))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "IdAuthor", "FirstName", book.AuthorID);
            ViewData["BookCategoryID"] = new SelectList(_context.BookCategories, "CategoryID", "CategoryBook", book.BookCategoryID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "IdCategory", "KindOfBook", book.CategoryID);
            ViewData["PublishHouseID"] = new SelectList(_context.PublishHouses, "IdPublishHouse", "PublishHouseName", book.PublishHouseID);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookCategory)
                .Include(b => b.Category)
                .Include(b => b.PublishHouse)
                .FirstOrDefaultAsync(m => m.IdBook == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.IdBook == id);
        }
    }
}
