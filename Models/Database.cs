using System;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using LibraryManagementSystem.Models;
using System.Data;
namespace LibraryManagementSystem.Models{
    public class Database{
        static SqlConnection sqlconnection=new SqlConnection("Server=DESKTOP-SBSQNFC\\SQLEXPRESS;Database=LibraryManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true");

        static public string UserLogin(Login login)
        {
                
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("VerifyUser",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailID",login.EmailID);
                command.Parameters.AddWithValue("@Password", login.Password);
                int Count=Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
                if(Count==1)
                {
                  return "success";         
                }
                
                  return "fails";
                
                     
        }

         static public string UserRegister(Register register)
        {
                
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("UserRegister",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName",register.FirstName);
                command.Parameters.AddWithValue("@LastName",register.LastName);
                command.Parameters.AddWithValue("@EmailID",register.EmailID);
                command.Parameters.AddWithValue("@Password",register.Password);
                command.Parameters.AddWithValue("@ConfirmPassword", register.ConfirmPassword);
                int Count=Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
                if (Count != 0) {
                  return "success";         
                }
                
                  return "fails";       
        }


        static public string AdminLogin(Login login)
        {
                
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("VerifyAdmin",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailID",login.EmailID);
                command.Parameters.AddWithValue("@Password", login.Password);
                int Count=Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
                if(Count==1)
                {
                  return "success";         
                }
                
                  return "fails";      
        }
        /*static public string AdminRegister(Register register)
        {
                
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("AdminRegister",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName",register.FirstName);
                command.Parameters.AddWithValue("@LastName",register.LastName);
                command.Parameters.AddWithValue("@EmailID",register.EmailID);
                command.Parameters.AddWithValue("@Password",register.Password);
                command.Parameters.AddWithValue("@ConfirmPassword", register.ConfirmPassword);
                command.ExecuteNonQuery();
                sqlconnection.Close();
                if (Count != 0) {
                  return "success";         
                }
                
                  return "fails"  ;  
                  return "Success"; 
        }*/
    }
}