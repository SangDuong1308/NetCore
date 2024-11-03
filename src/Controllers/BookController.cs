using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using src.DTOs.Book;
using src.Models;
using src.Repository;

namespace src.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly BookRepository _bookRepository;
        public BookController(IMapper mapper, BookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBook() {
            try {
                var books = await _bookRepository.GetAllAsync();
                var bookList = _mapper.Map<List<BookResponseDto>>(books);
                if (bookList == null || !bookList.Any())
                {
                    return BadRequest("There's no book in the database");
                }
                return Ok(bookList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdWithCategory(string id) {
            try {
                var book = await _bookRepository.GetABookWithCategoryAsync(id);
                if (book == null)
                {
                    return NotFound("Book not found");
                }
                var bookResponse = _mapper.Map<BookResponseDto>(book);
                return Ok(bookResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest("Book data is required");
            }
            try
            {
                var book = _mapper.Map<Book>(bookDto);
                await _bookRepository.AddAsync(book);
                // return Ok("Book added successfully");
                return CreatedAtAction(nameof(GetBookByIdWithCategory), new { id = book.BookId }, new { BookId = book.BookId, message = "Book added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the book: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(string id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (updateBookDto == null)
                return BadRequest("Book is null.");

            try
            {
                var existingBook = await _bookRepository.GetByIdAsync(id);
                if (existingBook == null)
                {
                    return NotFound("Book not found.");
                }

                // var bookToUpdate = _mapper.Map<Book>(updateBookDto);
                // bookToUpdate.BookId = existingBook.BookId;
                _mapper.Map(updateBookDto, existingBook);
                await _bookRepository.UpdateAsync(existingBook);

                return Ok("Book updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the book: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            try
            {
                var isDeleted = await _bookRepository.DeleteAsync(id);
                if (!isDeleted)
                {
                    return NotFound("Book not found.");
                }
                return Ok("Book deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the book: " + ex.Message);
            }
        }
    }
}