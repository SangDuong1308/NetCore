using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Repository
{
    public class BookRepository : BaseRepository.BaseRepository<Book>
    {
        public BookRepository(ApplicationDBContext context) : base(context)
        {

        }

        public async Task<Book> GetABookWithCategoryAsync(string bookId)
        {
            return await _context.Books.Include(b => b.Category).Where(b => b.BookId == bookId).FirstOrDefaultAsync() ?? new Book();
        }
    }
}