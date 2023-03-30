using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Controllers
{
    public class CSharpBookController : Controller
    {
        private readonly CSharpBookDbContext db;
        private readonly IWebHostEnvironment _environment;
        public CSharpBookController(CSharpBookDbContext context, IWebHostEnvironment environment)
        {
            db = context;
            _environment = environment;
        }
        
  
        [HttpGet]

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)

        {



            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;


            var li = from s in db.CSharpBooks
                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.BookName.Contains(searchString)
                                       || s.AuthorName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.BookName);
                    break;
                case "Date":
                    li = li.OrderBy(s => s.AddedOn);
                    break;
                case "date_desc":
                    li = li.OrderByDescending(s => s.AddedOn);
                    break;
                default:
                    li = li.OrderBy(s => s.BookID);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<CSharpBook>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {

            CSharpBook u = new CSharpBook();

            return View(u);
        }

        [HttpPost]
        public async Task<IActionResult>  Create(CSharpBook ui, IFormFile file)
        {
            CSharpBook u = new CSharpBook();
            if (file == null || file.Length == 0)
            {
                u.ProfilePicture = "NoImage.png";
            }
            else
            {
                string filename = System.Guid.NewGuid().ToString() + " .jpg";
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "images", filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                u.ProfilePicture = filename;

            }


          
            u.BookID = ui.BookID;
            u.BookName = ui.BookName;
            u.BookEdition = ui.BookEdition;
            u.TotalPages = ui.TotalPages;
            u.Description = ui.Description;
            u.AuthorName = ui.AuthorName;
            u.AddedOn = DateTime.Now;
             db.CSharpBooks.Add(u);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)

        {

            if (id == null)

            {

                return NotFound();

            }



            CSharpBook u = await db.CSharpBooks.Where(x => x.ID == id).FirstOrDefaultAsync();



           

            if (u == null)

            {

                return NotFound();

            }



            return View(u);

        }







        [HttpPost]

        public async Task<IActionResult> Edit(int? id,CSharpBook ui, IFormFile file)

        {

            if (id == null)

            {

                return NotFound();

            }



            CSharpBook u = await db.CSharpBooks.Where(x => x.ID == id).FirstOrDefaultAsync();



            if (u == null)

            {

                return NotFound();

            }





            if (file != null || file.Length != 0)

            {



                string filename = System.Guid.NewGuid().ToString() + " .jpg";

                var path = Path.Combine(

                            Directory.GetCurrentDirectory(), "wwwroot", "images", filename);



                using (var stream = new FileStream(path, FileMode.Create))

                {

                    await file.CopyToAsync(stream);

                }



                u.ProfilePicture = filename;



            }
            u.BookID = ui.BookID;
            u.BookName = ui.BookName;
            u.BookEdition = ui.BookEdition;
            u.TotalPages = ui.TotalPages;
            u.Description = ui.Description;
            u.AuthorName = ui.AuthorName;
            u.AddedOn = DateTime.Now;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");



           

           

        }



[HttpGet]


        public async Task<IActionResult> Details(int? id)


        {

            if (id == null)

            {

                return NotFound();

            }



            CSharpBook u = await db.CSharpBooks.Where(x => x.ID == id).FirstOrDefaultAsync();





            if (u == null)

            {

                return NotFound();

            }



            return View(u);

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.CSharpBooks == null)
            {
                return NotFound();
            }

            var CSharpBooks = await db.CSharpBooks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (CSharpBooks == null)
            {
                return NotFound();
            }

            return View(CSharpBooks);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.CSharpBooks == null)
            {
                return Problem("Entity set 'BookDbContext.Book'  is null.");
            }
            var CSharpBooks = await db.CSharpBooks.FindAsync(id);
            if (CSharpBooks != null)
            {
                db.CSharpBooks.Remove(CSharpBooks);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool BookExists(int id)
        {
          return (db.CSharpBooks?.Any(e => e.ID == id)).GetValueOrDefault();
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}