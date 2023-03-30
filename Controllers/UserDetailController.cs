using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;
using System.Net.Mail;

namespace LibraryManagementSystem.Controllers
{
    public class UserDetailController : Controller
    {
        private readonly UserDetailDbContext db;
        private readonly IWebHostEnvironment _environment;
        public UserDetailController(UserDetailDbContext context, IWebHostEnvironment environment)
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


            var li = from s in db.UserDetails
                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.Name.Contains(searchString)
                                       || s.EmailID.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.Name);
                    break;
                default:
                    li = li.OrderBy(s => s.RegisterNo);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<UserDetail>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {

            UserDetail u = new UserDetail();

            return View(u);
        }

        [HttpPost]
        public async Task<IActionResult>  Create(UserDetail ui, IFormFile file)
        {
            UserDetail u = new UserDetail();
            if (file == null || file.Length == 0)
            {
                u.ProfilePicture = "NoImage.png";
            }
            else
            {
                string filename = System.Guid.NewGuid().ToString() + " .png";
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "images", filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                u.ProfilePicture = filename;

            }


          
            u.RegisterNo = ui.RegisterNo;
            u.Name = ui.Name;
            u.EmailID = ui.EmailID;
            u.Password=ui.Password;
            u.Gender=ui.Gender;
            u.Age = ui.Age;
            u.Address = ui.Address;
            u.PhoneNumber = ui.PhoneNumber;
             db.UserDetails.Add(u);
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



            UserDetail u = await db.UserDetails.Where(x => x.ID == id).FirstOrDefaultAsync();



           

            if (u == null)

            {

                return NotFound();

            }



            return View(u);

        }







        [HttpPost]

        public async Task<IActionResult> Edit(int? id,UserDetail ui, IFormFile file)

        {

            if (id == null)

            {

                return NotFound();

            }



            UserDetail u = await db.UserDetails.Where(x => x.ID == id).FirstOrDefaultAsync();



            if (u == null)

            {

                return NotFound();

            }





            if (file != null || file.Length != 0)

            {



                string filename = System.Guid.NewGuid().ToString() + " .png";

                var path = Path.Combine(

                            Directory.GetCurrentDirectory(), "wwwroot", "images", filename);



                using (var stream = new FileStream(path, FileMode.Create))

                {

                    await file.CopyToAsync(stream);

                }



                u.ProfilePicture = filename;



            }
            u.RegisterNo = ui.RegisterNo;
            u.Name = ui.Name;
            u.EmailID = ui.EmailID;
            u.Password=ui.Password;
            u.Gender=ui.Gender;
            u.Age = ui.Age;
            u.Address = ui.Address;
            u.PhoneNumber = ui.PhoneNumber;
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



            UserDetail u = await db.UserDetails.Where(x => x.ID == id).FirstOrDefaultAsync();





            if (u == null)

            {

                return NotFound();

            }



            return View(u);

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.UserDetails == null)
            {
                return NotFound();
            }

            var UserDetails = await db.UserDetails
                .FirstOrDefaultAsync(m => m.ID == id);
            if (UserDetails == null)
            {
                return NotFound();
            }

            return View(UserDetails);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.UserDetails == null)
            {
                return Problem("Entity set 'UserDetailDbContext.UserDetail'  is null.");
            }
            var UserDetails = await db.UserDetails.FindAsync(id);
            if (UserDetails != null)
            {
                db.UserDetails.Remove(UserDetails);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool BookExists(int id)
        {
          return (db.UserDetails?.Any(e => e.ID == id)).GetValueOrDefault();
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



        public ActionResult SendMail() {  
                return View();  
            }  
            [HttpPost]  
        public ViewResult SendMail(LibraryManagementSystem.Models.Mail _objModelMail) {  
            if (ModelState.IsValid) {  
                MailMessage mail = new MailMessage();  
                mail.To.Add(_objModelMail.To);  
                mail.From = new MailAddress(_objModelMail.From);  
                mail.Subject = _objModelMail.Subject;  
                string Body = _objModelMail.Body;  
                mail.Body = Body;  
                mail.IsBodyHtml = true;  
                SmtpClient smtp = new SmtpClient();  
                smtp.Host = "smtp.gmail.com";  
                smtp.Port = 587;  
                smtp.UseDefaultCredentials = false;  
                smtp.Credentials = new System.Net.NetworkCredential("swedha0025@gmail.com", "bqnjrsgepqrvqhew"); // Enter seders User name and password  
                smtp.EnableSsl = true;  
                smtp.Send(mail);  
                return View("Index", _objModelMail);  
            } else {  
                return View();  
            }  
        }  
    }
}