using BooksApi.DAL;
using BooksApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext db;
        public BooksController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return db.Books;
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Book>>> Search(string query)
        {
            return await db.Books.Where(x => x.Name.Contains(query) || x.Author.Contains(query)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null) return NotFound();
            return book;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Book book)
        {
            await db.Books.AddAsync(book);
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Book book)
        {
            if (id != book.Id) return BadRequest();
            if (!db.Books.Any(x=>x.Id == id)) return NotFound();
            db.Update(book);
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!db.Books.Any(x => x.Id == id)) return NotFound();
            db.Books.Remove(await db.Books.FindAsync(id));
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}
