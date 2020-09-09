using KsiegarniaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiegarniaOnline.DAL
{
    public static class KsiegarniaInitializer
    {
        public static void Initialize(KsiegarniaOnlineContext context)
        {
            context.Database.EnsureCreated();

            if (context.Books.Any())
            {
                return;   // DB has been seeded
            }

            var bookCategory = new BookCategory[]
            {
                new BookCategory { CategoryBook = "Przygodowa" },
                new BookCategory { CategoryBook = "Informatyczna" },
                new BookCategory { CategoryBook = "Historyczna" }
            };
            foreach(BookCategory bc in bookCategory)
            {
                context.BookCategories.Add(bc);
            }
            context.SaveChanges();
            
            var books = new Book[]
            {
                new Book { IBSN = 1111111111, Title = "Ksiazka" , Autor="Ja", Publisher = "Moja mama", PublishDate = DateTime.Today , NumberOfPages = 401 },
                new Book { IBSN = 1111111111, Title = "Ksiazka" , Autor="Ja", Publisher = "Moja mama", PublishDate = DateTime.Today , NumberOfPages = 401 },
                new Book { IBSN = 1111111111, Title = "Ksiazka" , Autor="Ja", Publisher = "Moja mama", PublishDate = DateTime.Today , NumberOfPages = 401 }

            };
            foreach (Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();
        }
    }
}

