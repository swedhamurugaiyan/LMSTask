using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;  
using System.Configuration;  
using System.Data;    
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementSystem.Controllers;
public class LoginController:Controller{
    

    /*public IConfiguration Configuration { get; }
        public AdminController(IConfiguration configuration)
        {
            Configuration = configuration;
        }*/
        /*public IActionResult AdminLogin(){
            ClaimsPrincipal claimUser=HttpContext.User;
            if(claimUser.Identity.IsAuthenticated){
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        */


         
          public IActionResult Index()
    {
       
        return View();
    }

    [HttpGet]
    public IActionResult AdminLogin()
    {
       
        return View();
    }
    
    [HttpPost]
     public IActionResult AdminLogin(Login login)
    {
       string result= Database.AdminLogin(login);
       Console.WriteLine(result);
       if(result=="success")
       {
          HttpContext.Session.SetString("EmailID", login.EmailID);
          return RedirectToAction("AdminHomePage","Admin");
       }
      
       return RedirectToAction("AdminLogin","Login");
        
    }
    public IActionResult AdminRegister(Register register)
    {
        SqlConnection sqlconnection=new SqlConnection("Server=DESKTOP-SBSQNFC\\SQLEXPRESS;Database=LibraryManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true");
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("AdminRegister",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("FirstName",register.FirstName);
                command.Parameters.AddWithValue("LastName",register.LastName);
                command.Parameters.AddWithValue("EmailID",register.EmailID);
                command.Parameters.AddWithValue("Password",register.Password);
                command.Parameters.AddWithValue("ConfirmPassword", register.ConfirmPassword);
                
                int Count=Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
                if(Count==1)
                {
                  return View("AdminLogin");        
                }
                
                  return View("AdminLogin");
          //HttpContext.Session.SetString("EmailID ", register.EmailID);
          
       }
}

            
                        /*if(modelLogin.EmailID==EmailID && modelLogin.Password==Password){
                            List<Claim> claims=new List<Claim>(){
                                new Claim(ClaimTypes.NameIdentifier,EmailID),
                                new Claim("OtherProperties","Example Role")
                            };
                            ClaimsIdentity claimsIdentity=new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                            AuthenticationProperties properties=new AuthenticationProperties(){
                                AllowRefresh=true,
                                IsPersistent=modelLogin.KeepLoggedIn
                            };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),properties);
                            return RedirectToAction("Index","Home");
                        }
                        ViewData["ValidateMessage"]="User not found";*/
    

            
    
        /*[HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminRegister(AdminLogin adminLogin)
        {
                 List<AdminLogin> adminLogins = new List<AdminLogin>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into users (FirstName,LastName,EmailID,Password,ConfirmPassword) Values ('{adminLogin.FirstName}','{adminLogin.LastName}', '{adminLogin.EmailID}','{adminLogin.Password}','{adminLogin.ConfirmPassword}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            ViewBag.Result = "Success";
            return View();
        }*/
