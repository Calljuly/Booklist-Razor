using BooklistRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooklistRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private ApplicationContext _db;

        public BookController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.Book.ToList()});
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if(book == null)
            {
                return Json(new { success=false, message= "Error while loading" });
            }
             _db.Book.Remove(book);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Book deleted" });

        }
    }
}
