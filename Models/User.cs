using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class User
  {
    [Key]
    public int UserId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string StreetAddress { get; set; }

    [Required]
    public string City {get; set;}

    [Required]
    public string State {get; set;}

    [Required]
    public int ZipCode {get; set;}
    
    [Required]
    public ICollection<Product> Products;
    
  }
}
