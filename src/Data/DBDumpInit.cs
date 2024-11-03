using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Data
{
    public class DBDumpInit
    {
        public static void Initialize(ApplicationDBContext applicationDBContext)
        {
            applicationDBContext.Database.EnsureCreated();

            if (applicationDBContext.Categories.Any())
            {
                return;
            }

            var books = new Book[]
            {
                new Book{Title="Ashfall", Author="Mike Mullin"},
                new Book{Title="The Hunger Games", Author="Suzanne Collins"}
            };

            var categories = new Category[]
            {
                new Category{Name="Science Fiction"},
                new Category{Name="Dystopian"}
            };

            categories[0].Books.Add(books[0]);
            categories[1].Books.Add(books[1]);

            foreach(Category c in categories)
            {
                applicationDBContext.Categories.Add(c);
            }

            applicationDBContext.SaveChanges();
        }
    }
}