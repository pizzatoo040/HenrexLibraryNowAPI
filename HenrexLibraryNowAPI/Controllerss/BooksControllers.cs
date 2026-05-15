using HenrexLibraryNowAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace QuijanoLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Books> books = new List<Books>
        {
            new Books
            {
                Id = 1,
                Title = "Harry Potter",
                Author = "JK Rowling",
                Genre = "Fantasy",
                Available = true,
                PublishedYear = 1997

            },

            new Books
            {
                Id = 2,
                Title = "Game of Thrones",
                Author = "George Martin",
                Genre = "Epic Fantasy",
                Available= true,
                PublishedYear = 1996

            }
        };
        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Books Retrieved"

            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });

            return Ok(new
            {
                status = "success",
                data = books,
                message = "Book Retrived"
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Books newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById),
                new { id = newBook.Id },
                new
                {
                    status = "success",
                    data = newBook,
                    message = "Book Created"
                });

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,
            [FromBody] Books Updatebook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (Object?)null,
                    message = "Book not Found"
                });
            book.Title = Updatebook.Title;
            book.Author = Updatebook.Author;
            book.Genre = Updatebook.Genre;
            book.Available = Updatebook.Available;
            book.PublishedYear = Updatebook.PublishedYear;

            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = (object?)null,
                message = "Book Update"
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (Object?)null,
                    message = "Book not Found"

                });


            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = (object?)null,
                message = "Book Deleted"
            });
        }
    }
}








