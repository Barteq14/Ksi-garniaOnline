using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KsiegarniaOnline.DAL;
using KsiegarniaOnline.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace KsiegarniaOnline.Controllers
{
    public class BooksController : Controller
    {
        private readonly KsiegarniaOnlineContext _context;

        public BooksController(KsiegarniaOnlineContext context)
        {
            _context = context;
        }
       [HttpGet]
        // GET: Books
        public async Task<IActionResult> Index(string searchString, string searchPrice, string sortOrder, string currentFilter, string currentFilter2)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["AuthorSortParm"] = sortOrder == "Author" ? "author_desc" : "Author";
            ViewData["WydawnictwoSortParm"] = sortOrder == "Wydawnictwo" ? "wydawnictwo_desc" : "Wydawnictwo";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilter2"] = searchPrice;

            

            var ksiegarniaOnlineContext = _context.Books.Include(b => b.Author).Include(b => b.BookCategory).Include(b => b.Category).Include(b => b.PublishHouse);
            //var cena = _context.Books.Where(o => o.Price <= searchInt).Select(o => o);

            var books = from b in ksiegarniaOnlineContext
                        select b;


            if (!String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(searchPrice))
            {
                String napis;
                    try
                    {
                        napis = searchString;
                        books = books.Where(p => p.Title.Contains(napis) ||
                        p.Author.FirstName.Contains(napis) ||
                        p.Category.KindOfBook.Contains(napis));


                        ViewBag.phrase = "Szukana fraza: " + napis + " | ";
                        ViewBag.count = "Ilość znalezionych pozycji: " + books.Count();
                    }
                    catch
                    {
                        ViewBag.komunikat = "Podaj tytuł lub autora!";
                    }

                }
                else if(!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchPrice))
                {
                    int price;
                    try
                    {
                        price = Int32.Parse(searchPrice);

                        if (price > 0)
                        {
                            books = books.Where(p => p.Title.Contains(searchString) &&
                            p.Price <= price);

                            ViewBag.phrase = "Szukana fraza: " + searchString + " | ";
                            ViewBag.count = "Ilość znalezionych pozycji: " + books.Count();
                            ViewBag.price = "Maksymalna cena: " + price + " | ";
                        }
                        else
                        {
                            ViewBag.error = "Podaj prawidłową cenę!";
                            ViewBag.phrase = "Szukana fraza: " + searchString + " | ";
                            ViewBag.count = "Ilość znalezionych pozycji: 0";
                            ViewBag.price = "Maksymalna cena: " + price + " | ";
                        }
                    }
                    catch
                    {
                        ViewBag.komunikat = "Podaj cyfrę!";
                    } 
                }
                else if(String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchPrice))
                {

                    int price;

                    try
                    {
                        price = Int32.Parse(searchPrice);

                        if (price > 0)
                        {
                            books = books.Where(p => p.Title.Contains(searchString) ||
                            p.Price <= price);


                            ViewBag.price = "Maksymalna cena: " + price + " | ";
                            ViewBag.count = "Ilość znalezionych pozycji: " + books.Count();
                        }
                        else
                        {
                            ViewBag.error = "Podaj prawidłową cenę!";
                            ViewBag.price = "Maksymalna cena: " + price + " | ";
                            ViewBag.count = "Ilość znalezionych pozycji: 0";
                        }
                    }
                    catch{
                        ViewBag.komunikat = "Podaj cyfrę!";
                    }

                }
               



            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(s => s.Title);
                    break;
                case "Price":
                    books = books.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    books = books.OrderByDescending(s => s.Price);
                    break;
                case "Author":
                    books = books.OrderBy(s => s.Author.FirstName);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(s => s.Author.FirstName);
                    break;
                case "Wydawnictwo":
                    books = books.OrderBy(s => s.PublishHouse.PublishHouseName);
                    break;
                case "wydawnictwo_desc":
                    books = books.OrderByDescending(s => s.PublishHouse.PublishHouseName);
                    break;
                default:
                    books = books.OrderBy(s => s.Title);
                    break;
            }

            return View(await  books.AsNoTracking().ToListAsync());
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
