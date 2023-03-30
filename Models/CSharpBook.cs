using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models;


public partial class CSharpBook
{
    [Key]
    public int ID { get; set; }
    
    public string? ProfilePicture { get; set; }

    public int BookID { get; set; }

    public string? BookName { get; set; }

    public int BookEdition { get; set; }

    public int TotalPages { get; set; }
    public string? Description { get; set; }
    public string? AuthorName { get; set; }

    public DateTime? AddedOn { get; set; }
}
