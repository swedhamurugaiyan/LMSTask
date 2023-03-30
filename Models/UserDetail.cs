using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models;


public partial class UserDetail
{
    [Key]
    public int ID { get; set; }
    

    
    public string? ProfilePicture { get; set; }


[Required(ErrorMessage = "Register number field is required.")]
    public int RegisterNo { get; set; }


[Required(ErrorMessage = "Name field is required.")]
    public string? Name { get; set; }

[Required(ErrorMessage = "Email ID field is required.")]
    public string? EmailID { get; set; }

[Required(ErrorMessage = "Password field is required.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Gender field is required.")]
    public string? Gender { get; set; }


[Required(ErrorMessage = "Age field is required.")]
    public int Age { get; set; }


    [Required(ErrorMessage = "Address field is required.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "phone number field is required.")]
    public string PhoneNumber { get; set; }

}
