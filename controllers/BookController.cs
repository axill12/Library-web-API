using Microsoft.AspNetCore.Mvc;
using Library_web_API.persistence;
using Library_web_API.persistence.model;
using Microsoft.EntityFrameworkCore;

namespace Library_web_API.controllers
{

    [ApiController]
    [Route("/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext context;

        public BookController (AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks ()
        {
            return await context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById (int id)
        {
            var book = await context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(id);
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook (Book book)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }
    }
}
