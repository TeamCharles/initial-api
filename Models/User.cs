using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  /**
   * Class: User
   * Purpose: Represents the User table in the database
   * Author: Garrett Vangilder
   */
  public class User
  {
    [Key]
    public int UserId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    [Display (Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display (Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [Display (Name = "Street Address")]
    public string StreetAddress { get; set; }

    [Required]
    public string City {get; set;}

    [Required]
    public string State {get; set;}

    [Required]
    [Display (Name = "Zip Code")]
    public int ZipCode {get; set;}
    
    public ICollection<Product> Products;

    public ICollection<PaymentType> PaymentTypes;

  }
}
